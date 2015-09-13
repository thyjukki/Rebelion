﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {

    private Stack<ItemScript> items;

    public Stack<ItemScript> Items
    {
        get { return items; }
        set { items = value; }
    }

    private Image IconSprite;

    public Text stackTxt;


    public bool isEmpty
    {
        get { return items.Count == 0; }
    }

    public ItemScript CurrentItem
    {
        get { return items.Peek(); }
    }

    public bool IsAvailable
    {
        get { return CurrentItem.Item.MaxSize > items.Count; }
    }

    public Sprite ItemSprite
    {
        get { return IconSprite.sprite; }
    }


    private CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
        items = new Stack<ItemScript>();
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();
        RectTransform iconRect = transform.FindChild("ButtonIcon").GetComponent<RectTransform>();

        IconSprite = transform.FindChild("ButtonIcon").GetComponent<Image>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        //iconRect.localPosition = slotRect.localPosition;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

        iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        iconRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

        if (transform.parent != null)
        {
            canvasGroup = transform.parent.GetComponent<CanvasGroup>();
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddItem(ItemScript item)
    {
        items.Push(item);

        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        ChangeSprite(item.sprite);

        IconSprite.enabled = true;
    }

    public void AddItems(Stack<ItemScript> items)
    {
        this.items = new Stack<ItemScript>(items);

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        ChangeSprite(CurrentItem.sprite);
    }

    private void ChangeSprite (Sprite icon)
    {
        IconSprite.sprite = icon;
        IconSprite.enabled = true;
    }

    private void UseItem()
    {
        if (!this.isEmpty)
        {
            items.Pop().Use();

            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (this.isEmpty)
            {
                IconSprite.enabled = false;
            }
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        IconSprite.enabled = false;
        stackTxt.text = string.Empty;


    }

    /// <summary>
    /// Removes a single item from the slot and returns it
    /// </summary>
    /// <returns>Removed item</returns>
    public ItemScript RemoveItem()
    {
        ItemScript tmp;

        tmp = items.Pop();

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        if (isEmpty)
            ClearSlot();

        return tmp;
    }

    /// <summary>
    /// Removes given number of items and returns them
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Removed items</returns>
    public Stack<ItemScript> RemoveItems(int amount)
    {
        Stack<ItemScript> tmp = new Stack<ItemScript>();

        for (int i = 0; i < amount; i++)
        {
            tmp.Push(items.Pop());
        }

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        if (isEmpty)
            ClearSlot();

        return tmp;
    }

    /// <summary>
    /// Removes all the items from the slot
    /// </summary>
    /// <returns>All items in the slot</returns>
    public Stack<ItemScript> RemoveAllItems()
    {
        Stack<ItemScript> tmp = new Stack<ItemScript>(items);
        ClearSlot();
        stackTxt.text = string.Empty;

        return tmp;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("hover") && canvasGroup.alpha == 1)
        {
            UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Left && Input.GetKey(KeyCode.LeftShift) && items.Count > 0)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(InventoryManager.Instance.canvas.transform as RectTransform, Input.mousePosition, InventoryManager.Instance.canvas.worldCamera, out position);
            
            InventoryManager.Instance.selectStackSize.SetActive(true);
            
            InventoryManager.Instance.selectStackSize.transform.position = InventoryManager.Instance.canvas.transform.TransformPoint(position);
            InventoryManager.Instance.SetStackInfo(items.Count);
        }
    }
}
