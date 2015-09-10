using UnityEngine;
using System.Collections;

public enum Category { Equipment, Weapon, Consumable};
public class ItemManager : MonoBehaviour
{
    public ItemType itemType;
    public Quality quality;
    public Category category;
    public string spritePath;
    public int maxSize;
    public string itemName;
    public string description;
    public int strenght;
    public int dexterity;
    public int stamina;
    public int magic;
    public float attackSpeed;
    public int health;
    public int mana;
    
    public void CreateItem()
    {

    }
}
