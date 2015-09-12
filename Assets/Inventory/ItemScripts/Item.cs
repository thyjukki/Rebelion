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

    public abstract void Use();

    public virtual string GetTooltip()
    {
        return null;
    }
}
