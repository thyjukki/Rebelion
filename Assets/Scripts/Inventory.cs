using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventory : MonoBehaviour {

    private RectTransform inventoryRect;

    private float inventoryWidth, inventoryHeight;

    public int slots;

    public int rows;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize;


    protected CanvasGroup canvasGroup;

    //Slot to;

    public float fadeTime;


    private bool fadingIn;
    private bool fadingOut;


    protected bool isOpen;

    public bool IsOpen
    {
        get { return isOpen; }
        set { isOpen = value; }
    }

    private List<GameObject> allSlots;

    /*public int EmptySlots
    {
        get { return emptySlots; }
        set { emptySlots = value; }
    }*/

	// Use this for initialization
	void Start () {
        IsOpen = false;
        canvasGroup = GetComponent<CanvasGroup>();
        CreateLayout();
	}
	
	// Update is called once per frame
    void Update()
    {

        // Delete the object
        // TODO(Jukki) remove this?
        /*if (Input.GetMouseButtonUp(0))
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
        }*/

	    if (InventoryManager.Instance.HoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            position.Set(position.x+ 1, position.y + 1);
            InventoryManager.Instance.HoverObject.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
        }

	}

    /// <summary>
    /// Shows the inventory
    /// </summary>
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


    /// <summary>
    /// Shows the tooltip of the given slot
    /// </summary>
    /// <param name="slot"></param>
    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        if (slot.GetComponentInParent<Inventory>().isOpen && !tmpSlot.isEmpty && InventoryManager.Instance.HoverObject == null)
        {
            InventoryManager.Instance.toolTipObject.SetActive(true);

            float xPos = slot.transform.position.x;
            float yPos = slot.transform.position.y;

            InventoryManager.Instance.visualTextObject.text = InventoryManager.Instance.sizeTextObject.text = tmpSlot.CurrentItem.GetToolTip();
            InventoryManager.Instance.toolTipObject.transform.position = new Vector2(xPos, yPos);
        }

    }


    /// <summary>
    /// Hides the tool tip
    /// </summary>
    public void HideToolTip()
    {
        InventoryManager.Instance.toolTipObject.SetActive(false);
    }



    private int EmptySlots()
    {
        int count = 0;

        foreach (GameObject tmp in allSlots)
        {
            Slot slot = tmp.GetComponent<Slot>();
            if (slot.isEmpty)
                count++;
        }
        return count;
    }

    /// <summary>
    /// Saves the inventory to PlayerPrefs
    /// 
    /// NOT DONE
    /// </summary>
    public void SaveInventory()
    {
        string content = string.Empty;

        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();

            if (!tmp.isEmpty)
            {
                content += i + "-" + tmp.CurrentItem.Item.GetType().ToString() + "-" + tmp.CurrentItem.Item.Id + "-" + tmp.Items.Count.ToString() + ";";
            }
        }

        Debug.Log(content);
        PlayerPrefs.SetString("content", content);
        PlayerPrefs.SetInt("slots", slots);
        PlayerPrefs.SetFloat("rows", rows);
        PlayerPrefs.SetFloat("slotPaddengLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat("slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat("slotSize", slotSize);

        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Loads the inventory from PlayerPrefs
    /// 
    /// NOT DONE
    /// </summary>
    /*public void LoadInventory()
    {
        string content = PlayerPrefs.GetString("content");

        slots = PlayerPrefs.GetInt("slots");
        rows = PlayerPrefs.GetInt("rows");
        slotPaddingLeft = PlayerPrefs.GetFloat("slotPaddingLeft");
        slotPaddingTop = PlayerPrefs.GetFloat("slotSize");

        CreateLayout();

        string[] splitContent = content.Split(';');

        foreach (string tmp in splitContent)
        {
            string[] splitValues = tmp.Split('-');

            if (splitValues.Length == 3)
            {
                int index = Int32.Parse(splitValues[0]);
                Category type = (Category)Enum.Parse(typeof(Category), splitValues[1]);
                int id = Int32.Parse(splitValues[2]);
                int count = Int32.Parse(splitValues[3]);

                ItemScript item = ItemScript.CreateItem(type, id);

                for (int i = 0; i < count; i++)
                {
                    //GameObject loadedItem = Instantiate(InventoryManager.Instance.itemObject);
                }
            }
        }
    }*/


    /// <summary>
    /// Create initial layout of the inventory
    /// </summary>
    public virtual void CreateLayout()
    {
        allSlots = new List<GameObject>();

        GridLayoutGroup glg = GetComponent<GridLayoutGroup>();

        glg.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        glg.constraintCount = rows;
        for (int i = 0; i < slots; i++)
        {
            GameObject newSlot = (GameObject)Instantiate(InventoryManager.Instance.slotPrefab);

            newSlot.name = "Slot";

            newSlot.transform.SetParent(this.transform);

            allSlots.Add(newSlot);
        }

        /*
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
        }*/
    }


    /// <summary>
    /// Place an item to first avaible empty slot
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if we were able to place the item in inventory</returns>
    private bool PlaceEmpty(ItemScript item)
    {
        if (EmptySlots() > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.isEmpty)
                {
                    tmp.AddItem(item);
                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Adds an item to inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true if we were able to add item to inventory</returns>
    public bool AddItem (ItemScript item)
    {
        //If we have unstackable item then just find the first avaible slot
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
                    if (tmp.CurrentItem.Item.Id == item.Item.Id && tmp.IsAvailable)
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
        if (EmptySlots() > 0)
        {
            PlaceEmpty(item);
        }
        return false;
    }


    /// <summary>
    /// Moves an item in the inventory
    /// </summary>
    /// <param name="clicked"></param>
    public void MoveItem(GameObject clicked)
    {
        InventoryManager IGInstance = InventoryManager.Instance;
        IGInstance.Clicked = clicked;

        CanvasGroup cg = clicked.transform.parent.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = clicked.transform.parent.parent.GetComponent<CanvasGroup>();
        if (!IGInstance.MovingSlot.isEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            if (tmp.isEmpty && (tmp.canContain ==  ItemType.Generic || tmp.canContain == IGInstance.MovingSlot.CurrentItem.Item.ItemType))
            {
                tmp.AddItems(IGInstance.MovingSlot.Items);
                IGInstance.MovingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.isEmpty && IGInstance.MovingSlot.CurrentItem.Item.Id == tmp.CurrentItem.Item.Id && tmp.IsAvailable)
            {
                MergeStacks(IGInstance.MovingSlot, tmp);
            }
            else if (tmp.canContain == IGInstance.MovingSlot.CurrentItem.Item.ItemType || tmp.canContain == ItemType.Generic)
            {
                Destroy(GameObject.Find("Hover"));
                CreateHoverIcon();
                Slot.SwapItems(IGInstance.MovingSlot, tmp);
            }
        }
        else if (clicked.transform.parent.GetComponent<Inventory>().isOpen && !Input.GetKey(KeyCode.LeftShift))
        {
            if (!clicked.GetComponent<Slot>().isEmpty)
            {
                IGInstance.MovingSlot.Items = clicked.GetComponent<Slot>().RemoveAllItems();
                /*InventoryManager.Instance.From = clicked.GetComponent<Slot>();
                InventoryManager.Instance.From.GetComponent<Image>().color = Color.gray;*/

                CreateHoverIcon();
            }
        }


    }


    /// <summary>
    /// Splits the stack
    /// </summary>
    public void SplitStack()
    {
        InventoryManager instance = InventoryManager.Instance;
        instance.selectStackSize.SetActive(false);

        /*if (instance.SplitAmount == instance.MaxStackCount)
        {
            MoveItem(instance.Clicked);
        }
        else */if (instance.SplitAmount > 0)
        {
            instance.MovingSlot.Items = instance.Clicked.GetComponent<Slot>().RemoveItems(instance.SplitAmount);

            CreateHoverIcon();
        }
    }


    /// <summary>
    /// Changes the number in the stack popup window
    /// </summary>
    /// <param name="i"></param>
    public void ChangeStacktText(int i)
    {
        InventoryManager.Instance.SplitAmount = i + Int32.Parse(InventoryManager.Instance.stackText.transform.parent.GetComponent<InputField>().text);

        if (InventoryManager.Instance.SplitAmount < 0)
        {
            InventoryManager.Instance.SplitAmount = 0;
        }

        if (InventoryManager.Instance.SplitAmount > InventoryManager.Instance.MaxStackCount)
        {
            InventoryManager.Instance.SplitAmount = InventoryManager.Instance.MaxStackCount;
        }

        InventoryManager.Instance.stackText.transform.parent.GetComponent<InputField>().text = InventoryManager.Instance.SplitAmount.ToString();
    }


    /// <summary>
    /// Merges 2 slots
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
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


    /// <summary>
    /// Creates the hover icon of the image being moved
    /// </summary>
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


    /// <summary>
    /// Places an item back to the slot is was being moved from
    /// </summary>
    private void PutItemBack()
    {
        /*if (InventoryManager.Instance.From != null)
        {
            Destroy(GameObject.Find("Hover"));
            InventoryManager.Instance.From.GetComponent<Image>().color = Color.white;
            InventoryManager.Instance.From = null;
        }
        else */
        if (!InventoryManager.Instance.MovingSlot.isEmpty)
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
