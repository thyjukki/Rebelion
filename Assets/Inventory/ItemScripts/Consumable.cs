using UnityEngine;
using System.Collections;

public class Consumable : Item
{
    public int Health { get; set; }

    public int Mana { get; set; }

    public Consumable()
    {

    }

    public Consumable(int id, string itemName,string description,
        ItemType itemType, Quality quality,
        string spritePath, int maxSize, int health, int mana)
        : base(id, itemName, description,
        itemType, quality,
        spritePath, maxSize)
    {
        this.Health = health;
        this.Mana = mana;
    }

    public override void Use()
    {

    }

    public override string GetTooltip()
    {
        return base.GetTooltip();
    }
}
