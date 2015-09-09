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


    private static CanvasGroup canvasGroup;

    Slot to;
    public static CanvasGroup CanvasGroup
    {
        get { return Inventory.canvasGroup; }
    }

    public float fadeTime;

    private bool fadingIn;
    private bool fadingOut;

    private float hoverYOffset;



    private List<GameObject> allSlots;

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
            if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && InventoryManager.Instance.From != null)
            {
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
                InventoryManager.Instance.From.ClearSlot();
                Destroy(GameObject.Find("Hover"));
                to = null;
                InventoryManager.Instance.From = null;
                emptySlots++;
            }
        }

	    if (InventoryManager.Instance.HoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            InventoryManager.Instance.HoverObject.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
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

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        if (!tmpSlot.isEmpty && InventoryManager.Instance.HoverObject == null)
        {
            InventoryManager.Instance.toolTipObject.SetActive(true);

            float xPos = slot.transform.position.x + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;

            InventoryManager.Instance.visualTextObject.text = InventoryManager.Instance.sizeTextObject.text = tmpSlot.CurrentItem.GetToolTip();
            InventoryManager.Instance.toolTipObject.transform.position = new Vector2(xPos, yPos);
        }
        
    }

    public void HideToolTip()
    {
        InventoryManager.Instance.toolTipObject.SetActive(false);
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
                GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x)
                    , -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * InventoryManager.Instance.canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * InventoryManager.Instance.canvas.scaleFactor);
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
        if (!item.stackable)
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
        if (InventoryManager.Instance.From == null && canvasGroup.alpha == 1)
        {
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;

                InventoryManager.Instance.HoverObject = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);
                InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = clicked.transform.FindChild("ButtonIcon").GetComponent<Image>().sprite;
                InventoryManager.Instance.HoverObject.name = "Hover";

                RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
                RectTransform clickedTranfsorm = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTranfsorm.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTranfsorm.sizeDelta.y);

                InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform);

                InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.From.gameObject.transform.localScale;
            }
        }
        else if (to == null)
        {
            to = clicked.GetComponent<Slot>();

            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && InventoryManager.Instance.From != null)
        {
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            to.AddItems(InventoryManager.Instance.From.Items);

            if (tmpTo.Count == 0)
            {
                InventoryManager.Instance.From.ClearSlot();
            }
            else
            {
                InventoryManager.Instance.From.AddItems(tmpTo);
            }

            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            to = null;
            InventoryManager.Instance.From = null;

            Destroy(GameObject.Find("Hover"));
        }


    }

    private void PutItemBack()
    {
        if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;
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
