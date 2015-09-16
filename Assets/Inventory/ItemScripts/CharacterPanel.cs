using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterPanel : Inventory
{

    public Slot[] equipmentSlots;

    private static CharacterPanel instance;

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



    void Awake()
    {
        equipmentSlots = transform.GetComponentsInChildren<Slot>();
    }

	// Use this for initialization
    void Start()
    {
        IsOpen = true;
        canvasGroup = GetComponentInParent<CanvasGroup>();
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
        }
    }


}
