using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

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
        ItemContainer itemContainer = new ItemContainer();

        Type[] itemTypes = {typeof(Equipment), typeof(Weapon), typeof(Consumable)};

        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer), itemTypes);

        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Items.xml"), FileMode.Open);

        itemContainer = (ItemContainer)serializer.Deserialize(fs);

        serializer.Serialize(fs, itemContainer);
    }
}
