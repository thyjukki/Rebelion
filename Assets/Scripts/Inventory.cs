using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;

    private float inventoryWidth, inventoryHeight;

    public int slots;

    public int rows;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize;

    public GameObject slotPrefab;

    public GameObject iconPrefab;

    public Canvas canvas;

    private static CanvasGroup canvasGroup;

    public static CanvasGroup CanvasGroup
    {
        get { return Inventory.canvasGroup; }
    }

    public float fadeTime;

    private bool fadingIn;
    private bool fadingOut;

    public EventSystem eventSystem;

    private float hoverYOffset;

    private static GameObject hoverObject;

    private List<GameObject> allSlots;

    private static Slot from, to;

    private static int emptySlots;

    public static int EmptySlots
    {
        get { return emptySlots; }
        set { emptySlots = value; }
    }

	// Use this for initialization
	void Start () {

        canvasGroup = transform.parent.GetComponent<CanvasGroup>();
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;
                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                to = null;
                from = null;
                emptySlots++;
            }
        }

	    if (hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canvasGroup.alpha > 0)
            {
                StartCoroutine("FadeOut");
                PutItemBack();
            }
            else
            {
                StartCoroutine("FadeIn");
            }

        }
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        emptySlots = slots;

        hoverYOffset = slotSize * 0.01f;

        inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

        inventoryRect = GetComponent<RectTransform>();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

        int colums = slots / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colums; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x)
                    , -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize *canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);
                newSlot.transform.SetParent(this.transform);

                allSlots.Add(newSlot);
            }
        }
    }

    private bool PlaceEmpty(Item item)
    {
        if (emptySlots > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.isEmpty)
                {
                    tmp.AddItem(item);
                    emptySlots--;
                    return true;
                }
            }
        }

        return false;
    }

    public bool AddItem (Item item)
    {
        if (item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }
        else
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (!tmp.isEmpty)
                {
                    if (tmp.CurrentItem.type == item.type && tmp.IsAvailable)
                    {
                        tmp.AddItem(item);
                        return true;
                    }
                }
            }
        }
        if (emptySlots > 0)
        {
            PlaceEmpty(item);
        }
        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        if (from == null && canvasGroup.alpha == 0)
        {
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                from = clicked.GetComponent<Slot>();
                from.GetComponent<Image>().color = Color.gray;

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.transform.FindChild("ButtonIcon").GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTranfsorm = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTranfsorm.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTranfsorm.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform);

                hoverObject.transform.localScale = from.gameObject.transform.localScale;
            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slot>();

            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && from != null)
        {
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            to.AddItems(from.Items);

            if (tmpTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }

            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;

            Destroy(GameObject.Find("Hover"));
        }


    }

    private void PutItemBack()
    {
        if (from != null)
        {
            Destroy(GameObject.Find("Hover"));
            from.GetComponent<Image>().color = Color.white;
            from = null;
        }
    }

    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");

            float startAlpha = canvasGroup.alpha;

            float rate = 1.0f / fadeTime;

            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = 0;
            fadingOut = false;
        }
    }

    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");

            float startAlpha = canvasGroup.alpha;

            float rate = 1.0f / fadeTime;

            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasGroup.alpha = 1;
            fadingIn = false;
        }
    }
}
