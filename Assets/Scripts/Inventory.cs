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


    private CanvasGroup canvasGroup;

    Slot to;

    public float fadeTime;


    private bool fadingIn;
    private bool fadingOut;

    private float hoverYOffset;

    private bool isOpen;

    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }

    private List<GameObject> allSlots;

    private int emptySlots;

    public int EmptySlots
    {
        get { return emptySlots; }
        set { emptySlots = value; }
    }

	// Use this for initialization
	void Start () {
        IsOpen = false;
        canvasGroup = GetComponent<CanvasGroup>();
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonUp(0))
        {
            if (!InventoryManager.Instance.eventSystem.IsPointerOverGameObject(-1) && InventoryManager.Instance.From != null && IsOpen)
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

	}

    public void Open()
    {
        if (canvasGroup.alpha > 0)
        {
            StartCoroutine("FadeOut");
            PutItemBack();
            HideToolTip();

            IsOpen = false;
        }
        else
        {
            StartCoroutine("FadeIn");
            IsOpen = true;
        }
    }

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        if (slot.GetComponentInParent<Inventory>().isOpen && !tmpSlot.isEmpty && InventoryManager.Instance.HoverObject == null)
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

    private bool PlaceEmpty(ItemScript item)
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

    public bool AddItem (ItemScript item)
    {
        if (item.Item.MaxSize == 1)
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
                        if (!InventoryManager.Instance.MovingSlot.isEmpty && InventoryManager.Instance.Clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else
                        {
                            tmp.AddItem(item);
                            return true;
                        }
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
        InventoryManager.Instance.Clicked = clicked;

        if (!InventoryManager.Instance.MovingSlot.isEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            if (tmp.isEmpty)
            {
                tmp.AddItems(InventoryManager.Instance.MovingSlot.Items);
                InventoryManager.Instance.MovingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.isEmpty && InventoryManager.Instance.MovingSlot.CurrentItem.type == tmp.CurrentItem.type && tmp.IsAvailable)
            {
                MergeStacks(InventoryManager.Instance.MovingSlot, tmp);
            }
        }
        else if (InventoryManager.Instance.From == null && clicked.transform.parent.GetComponent<Inventory>().isOpen && !Input.GetKey(KeyCode.LeftShift))
        {
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;

                CreateHoverIcon();
            }
        }
        else if (to == null && !Input.GetKey(KeyCode.LeftShift))
        {
            to = clicked.GetComponent<Slot>();

            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && InventoryManager.Instance.From != null)
        {
            Stack<ItemScript> tmpTo = new Stack<ItemScript>(to.Items);
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

    public void SplitStack()
    {
        InventoryManager instance = InventoryManager.Instance;
        instance.selectStackSize.SetActive(false);

        if (instance.SplitAmount == instance.MaxStackCount)
        {
            MoveItem(instance.Clicked);
        }
        else if (instance.SplitAmount > 0)
        {
            instance.MovingSlot.Items = instance.Clicked.GetComponent<Slot>().RemoveItems(instance.SplitAmount);

            CreateHoverIcon();
        }
    }

    public void ChangeStacktText (int i)
    {
        InventoryManager.Instance.SplitAmount += i;

        if (InventoryManager.Instance.SplitAmount < 0)
        {
            InventoryManager.Instance.SplitAmount = 0;
        }

        if (InventoryManager.Instance.SplitAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SplitAmount = InventoryManager.Instance.MaxStackCount;
        }
    }

    public void MergeStacks(Slot source, Slot destination)
    {
        int max = destination.CurrentItem.Item.MaxSize - destination.Items.Count;

        int count = source.Items.Count < max ? source.Items.Count : max;

        for (int i = 0; i < count; i++)
        {
            destination.AddItem(source.RemoveItem());
            InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count.ToString();
        }

        if (source.Items.Count == 0)
        {
            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }

    private void CreateHoverIcon()
    {
        InventoryManager.Instance.HoverObject = (GameObject)Instantiate(InventoryManager.Instance.iconPrefab);
        InventoryManager.Instance.HoverObject.GetComponent<Image>().sprite = InventoryManager.Instance.Clicked.transform.FindChild("ButtonIcon").GetComponent<Image>().sprite;
        InventoryManager.Instance.HoverObject.name = "Hover";

        RectTransform hoverTransform = InventoryManager.Instance.HoverObject.GetComponent<RectTransform>();
        RectTransform clickedTranfsorm = InventoryManager.Instance.Clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTranfsorm.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTranfsorm.sizeDelta.y);

        InventoryManager.Instance.HoverObject.transform.SetParent(GameObject.Find("Canvas").transform);

        InventoryManager.Instance.HoverObject.transform.localScale = InventoryManager.Instance.Clicked.gameObject.transform.localScale;

        InventoryManager.Instance.HoverObject.transform.GetChild(0).GetComponent<Text>().text = InventoryManager.Instance.MovingSlot.Items.Count > 1 ? InventoryManager.Instance.MovingSlot.Items.Count.ToString() : string.Empty;
    }

    private void PutItemBack()
    {
        if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;
        }
        else if (!InventoryManager.Instance.MovingSlot.isEmpty)
        {
            Destroy(GameObject.Find("Hover"));
            foreach (ItemScript item in InventoryManager.Instance.MovingSlot.Items)
            {
                InventoryManager.Instance.Clicked.GetComponent<Slot>().AddItem(item);
            }

            InventoryManager.Instance.MovingSlot.ClearSlot();
        }

        InventoryManager.Instance.selectStackSize.SetActive(false);
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
