using UnityEngine;
using System.Collections;


public enum ItemType {
    Consumable,
    Mainhand,
    Twohand,
    Offhand,
    Head,
    Neck,
    Chest,
    Ring,
    Legs,
    Bracers,
    Boots,
    Trinket
};

public enum Quality { Common, Uncommon, Rare, Epic, Legendary, Artifact };

public class ItemScript : MonoBehaviour
{

    public ItemType type;

    public Sprite sprite;

    private Item item;

    public Item Item
    {
        get { return item; }
        set {
            item = value;

            sprite = Resources.Load<Sprite>(value.SpritePath);
        }
    }

    public void Use()
    {
        /*switch (type)
        {
            case ItemType.Mana:
                Debug.Log("I just used a mana potion");
                break;
            case ItemType.Health:
                Debug.Log("I just used a health potion");
                break;
        }*/
    }

    public string GetToolTip()
    {
        return item.GetTooltip();
    }

    public static ItemScript CreateItem(Category type, int id)
    {
        GameObject tmp = Instantiate(InventoryManager.Instance.itemObject);
        tmp.AddComponent<ItemScript>();
        ItemScript itemScript = tmp.GetComponent<ItemScript>();


        Destroy(tmp);
        switch (type)
        {
            case Category.Equipment:
                itemScript.Item = InventoryManager.Instance.ItemContainer.Equipments.Find(x => x.Id == id);
                break;
            case Category.Weapon:
                itemScript.Item = InventoryManager.Instance.ItemContainer.Weapons.Find(x => x.Id == id);
                break;
            case Category.Consumable:
                itemScript.Item = InventoryManager.Instance.ItemContainer.Consumables.Find(x => x.Id == id);
                break;
            default:
                break;
        }

        return itemScript;
    }
}
