using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer {

    private List<Item> weapons = new List<Item>();
    private List<Item> consumables = new List<Item>();
    private List<Item> equipments = new List<Item>();


    public List<Item> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }

    public List<Item> Consumables
    {
        get { return consumables; }
        set { consumables = value; }
    }

    public List<Item> Equipments
    {
        get { return equipments; }
        set { equipments = value; }
    }

    public List<Item> AllItems()
    {
        List<Item> collector = new List<Item>(weapons);
        collector.AddRange(consumables);
        collector.AddRange(equipments);
        return collector;
    }


}
