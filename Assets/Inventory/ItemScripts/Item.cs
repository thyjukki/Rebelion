using UnityEngine;
using System.Collections;

public abstract class Item
{
    public int Id { get; set; }

    public ItemType ItemType { get; set; }
    public Quality Quality { get; set; }

    public string SpritePath { get; set; }

    public string ItemName { get; set; }
    public string Description { get; set; }

    public int MaxSize { get; set; }

    public Item()
    {

    }

    public Item(int id, string itemName, string description,
        ItemType itemType, Quality quality,
        string spritePath, int maxSize)
    {
        this.Id = id;
        this.Description = description;
        this.ItemName = itemName;
        this.ItemType = itemType;
        this.Quality = quality;
        this.SpritePath = spritePath;
        this.MaxSize = maxSize;
    }

    public abstract void Use(Slot slot, ItemScript item);

    public virtual string GetTooltip()
    {
        string color = string.Empty;
        string newLine = string.Empty;

        if (Description != string.Empty)
        {
            newLine = "\n";
        }

        switch (Quality)
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

        return string.Format("<color=" + color +
            "><size=10>{0}</size></color><size=8><i><color=lime>"
            + newLine + "{1}</color></i></size>"
            , ItemName, Description);
    }
}
