using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    public int Attack { get; set; }
    public int Defence { get; set; }

    public Weapon()
    {

    }

    public Weapon(int id, string itemName, string description,
        ItemType itemType, Quality quality,
        string spritePath, int maxSize
        , int strenght, int dexterity, int stamina, int magic, int attack , int defence)
        : base(id, itemName, description,
        itemType, quality,
        spritePath, maxSize,
        strenght, dexterity, stamina, magic)
    {
        this.Attack = attack;
        this.Defence = defence;
    }

    public override void Use()
    {

    }

    public override string GetTooltip()
    {
        string stats = string.Empty;
        string itemTip = base.GetTooltip();
        
        if (Attack > 0)
        {
            stats += "\n+" + Attack.ToString() + " Attack";
        }
        else if (Attack < 0)
        {
            stats += "\n" + Attack.ToString() + " Attack";
        }
        
        if (Defence > 0)
        {
            stats += "\n+" + Defence.ToString() + " Defence";
        }
        else if (Defence < 0)
        {
            stats += "\n" + Defence.ToString() + " Defence";
        }

        return string.Format("{0}" + "<size=10>{1}</size>", itemTip, stats);
    }
}
