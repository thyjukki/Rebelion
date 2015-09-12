using UnityEngine;
using System.Collections;

public class Equipment : Item
{
    public int Strenght { get; set; }
    public int Dexterity { get; set; }
    public int Stamina { get; set; }
    public int Magic { get; set; }

    public Equipment()
    {

    }

    public Equipment(int id, string itemName, string description,
        ItemType itemType, Quality quality,
        string spritePath, int maxSize
        , int strenght, int dexterity, int stamina, int magic)
        : base(id, itemName, description,
        itemType, quality,
        spritePath, maxSize)
    {
        this.Strenght = strenght;
        this.Dexterity = dexterity;
        this.Stamina = stamina;
        this.Magic = magic;
    }

    public override void Use()
    {

    }

    public override string GetTooltip()
    {
        return base.GetTooltip();
    }
}
