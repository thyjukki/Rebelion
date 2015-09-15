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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void CreateLayout()
    {
    }

    public void EquipItem(Slot slot, ItemScript item)
    {
        Slot.SwapItems(slot, Array.Find(equipmentSlots, x => x.canContain == item.Item.ItemType));
    }
}
