using UnityEngine;
using System.Collections;
using System;

public class EquipmentSlot : MonoBehaviour {

    public ItemScript item = null;

    public ItemScript Item
    {
      get { return item; }
    }

    private SpriteRenderer spriteRenderer;

    public Sprite[] subSprites;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

	    if (item == null)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            SetItem(item);
        }
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        if (spriteRenderer.enabled)
        {
            Sprite newSprite = Array.Find(subSprites, item => item.name == spriteRenderer.sprite.name);

            if (newSprite)
                spriteRenderer.sprite = newSprite;
        }
	
	}

    public bool IsEmpty()
    {
        //Debug.Log("is empty item " + item.ToString());
        return (item == null);
    }

    public void RemoveItem()
    {
        spriteRenderer.enabled = false;
        item = null;
        subSprites = null;
    }

    public void SetItem(ItemScript newItem)
    {
        item = newItem;
        string path;
        Equipment equipment = (Equipment)item.Item;

        if (!this.GetComponentInParent<HumanoidBuilder>().female || equipment.FemalePath == string.Empty)
            path = equipment.MalePath;
        else
            path = equipment.FemalePath;

        subSprites = Resources.LoadAll<Sprite>(path);

        spriteRenderer.enabled = true;
    }
}
