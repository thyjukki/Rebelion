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

    public override void Use(Slot slot, ItemScript item)
    {

    }

    public override string GetTooltip()
    {
        string stats = string.Empty;
        if (Mana > 0)
        {
            stats += "\n Restores " + Mana.ToString() + " Mana";
        }

        if (Health > 0)
        {
            stats += "\n Restores " + Health.ToString() + " Health";
        }

        string itemTip = base.GetTooltip();
        return string.Format("{0}" + "<size=10>{1}</size>", itemTip, stats);
    }
}
