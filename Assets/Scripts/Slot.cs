using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler {

    private Stack<Item> items;

    public Stack<Item> Items
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

    public Item CurrentItem
    {
        get { return items.Peek(); }
    }

    public bool IsAvailable
    {
        get { return CurrentItem.stackable || items.Count == 0; }
    }

    public Sprite ItemSprite
    {
        get { return IconSprite.sprite; }
    }

	// Use this for initialization
	void Start () {
        items = new Stack<Item>();
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
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void AddItem(Item item)
    {
        items.Push(item);

        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }

        ChangeSprite(item.sprite);

        IconSprite.enabled = true;
    }

    public void AddItems(Stack<Item> items)
    {
        this.items = new Stack<Item>(items);

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
                Inventory.EmptySlots++;
            }
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        IconSprite.enabled = false;
        stackTxt.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("hover") && Inventory.CanvasGroup.alpha == 1)
        {
            UseItem();
        }
    }
}
