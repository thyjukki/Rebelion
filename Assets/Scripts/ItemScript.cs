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
        get { return Item; }
        set { Item = value; }
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

        /*string stats = string.Empty;
        string color = string.Empty;
        string newLine = string.Empty;

        if (description != string.Empty)
        {
            newLine = "\n";
        }

        switch (quality)
	    {
		    case Quality.Common:
                color = "white";
                break;
            case Quality.Uncommon:
                color = "lime";
                break;
            case Quality.Rare:
                color = "navy";
                break;
            case Quality.Epic:
                color = "magenta";
                break;
            case Quality.Legendary:
                color = "orange";
                break;
            case Quality.Artifact:
                color = "red";
                break;
            default:
                break;
	    }
        
        if (strenght > 0)
        {
            stats +="\n+"+strenght.ToString() + " Strenght";
        }
        else if (strenght < 0)
        {
            stats += "\n" + strenght.ToString() + " Strenght";
        }
        if (dexterity > 0)
        {
            stats += "\n+" + dexterity.ToString() + " Dexterity";
        }
        else if (dexterity < 0)
        {
            stats += "\n" + dexterity.ToString() + " Dexterity";
        }
        if (stamina > 0)
        {
            stats += "\n+" + stamina.ToString() + " Stamina";
        }
        else if (stamina < 0)
        {
            stats += "\n" + stamina.ToString() + " Stamina";
        }
        if (mana > 0)
        {
            stats += "\n+" + mana.ToString() + " Mana";
        }
        else if (mana < 0)
        {
            stats += "\n" + mana.ToString() + " Mana";
        }

        return string.Format("<color=" + color +
            "><size=16>{0}</size></color><size=14><i><color=lime>"
            +newLine+"{1}</color></i>{2}</size>"
            , itemName, description, stats);*/
    }
}
