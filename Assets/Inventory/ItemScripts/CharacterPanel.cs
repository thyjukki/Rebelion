using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterPanel : Inventory
{

    public Slot[] equipmentSlots;

    private Dictionary<string, Slot> slotByNames;

    private static CharacterPanel instance;

    public GameObject player;

    public static CharacterPanel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }
            return instance;
        }
    }

	// Use this for initialization
    void Start()
    {
        slotByNames = new Dictionary<string, Slot>();

        IsOpen = true;
        canvasGroup = GetComponentInParent<CanvasGroup>();
        equipmentSlots = transform.GetComponentsInChildren<Slot>();

        foreach (Slot slot in equipmentSlots)
        {
            slotByNames.Add(slot.name, slot);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void CreateLayout()
    {
    }

    public void EquipItem(Slot slot, ItemScript item)
    {
        Slot to = Array.Find(equipmentSlots, x => x.canContain == item.Item.ItemType);
        if (to != null)
        {
            Slot.SwapItems(slot, to);
            SetClothing(to.name);
        }
    }

    public void EquipItem(ItemScript item)
    {
        Slot to = Array.Find(equipmentSlots, x => x.canContain == item.Item.ItemType);
        if (to != null)
        {
            to.AddItem(item);
            SetClothing(to.name);
        }
    }

    public Slot GetSlot(string name)
    {
        return slotByNames[name];
    }


    /// <summary>
    /// Otherwise do the same thing as MoveItem in inventory Script.
    /// But we need to reconstruct the invetory to the player.
    /// </summary>
    /// <param name="clicked"></param>
    public override void MoveItem(GameObject clicked)
    {
        base.MoveItem(clicked);

        foreach (Slot slot in equipmentSlots)
        {

            Transform equipmentTransform = player.transform.FindChild(slot.name);
            if (equipmentTransform != null)
            {
                EquipmentSlot equipment = equipmentTransform.GetComponent<EquipmentSlot>();

                if (slot.isEmpty && !equipment.IsEmpty())
                {
                    equipment.RemoveItem();
                }
                else if (!slot.isEmpty)
                {
                    if (equipment.IsEmpty() || equipment.Item.Item != slot.CurrentItem.Item)
                    {
                        equipment.SetItem(slot.CurrentItem);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reconstruct single slot's equipment stats.
    /// </summary>
    /// <param name="clothing"></param>
    private void SetClothing(string clothing)
    {
        EquipmentSlot equipment = player.transform.FindChild(clothing).GetComponent<EquipmentSlot>();
        Slot slot = GetSlot(clothing);

        if (slot.isEmpty && !equipment.IsEmpty())
        {
            equipment.RemoveItem();
        }
        else if (!slot.isEmpty)
        {
            if (equipment.Item == null || equipment.Item.Item != slot.CurrentItem.Item)
            {
                equipment.SetItem(slot.CurrentItem);
            }
        }
    }
}
