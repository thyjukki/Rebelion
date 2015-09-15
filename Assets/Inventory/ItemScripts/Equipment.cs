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

    public override void Use(Slot slot, ItemScript item)
    {
        CharacterPanel.Instance.EquipItem(slot, item);
    }

    public override string GetTooltip()
    {
        string stats = string.Empty;

        if (Strenght > 0)
        {
            stats += "\n+" + Strenght.ToString() + " Strenght";
        }
        else if (Strenght < 0)
        {
            stats += "\n" + Strenght.ToString() + " Strenght";
        }
        if (Dexterity > 0)
        {
            stats += "\n+" + Dexterity.ToString() + " Dexterity";
        }
        else if (Dexterity < 0)
        {
            stats += "\n" + Dexterity.ToString() + " Dexterity";
        }
        if (Stamina > 0)
        {
            stats += "\n+" + Stamina.ToString() + " Stamina";
        }
        else if (Stamina < 0)
        {
            stats += "\n" + Stamina.ToString() + " Stamina";
        }
        if (Magic > 0)
        {
            stats += "\n+" + Magic.ToString() + " Magic";
        }
        else if (Magic < 0)
        {
            stats += "\n" + Magic.ToString() + " Magic";
        }

        string itemTip = base.GetTooltip();
        return string.Format("{0}" + "<size=10>{1}</size>", itemTip, stats);
    }
}
