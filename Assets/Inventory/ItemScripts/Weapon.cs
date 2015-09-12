using UnityEngine;
using System.Collections;

public class Weapon : Equipment
{
    public float AttackSpeed { get; set; }

    public Weapon()
    {

    }

    public Weapon(int id, string itemName, string description,
        ItemType itemType, Quality quality,
        string spritePath, int maxSize
        , int strenght, int dexterity, int stamina, int magic, float attackSpeed)
        : base(id, itemName, description,
        itemType, quality,
        spritePath, maxSize,
        strenght, dexterity, stamina, magic)
    {
        this.AttackSpeed = attackSpeed;
    }

    public override void Use()
    {

    }

    public override string GetTooltip()
    {
        return base.GetTooltip();
    }
}
