using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EquipmentPreview : MonoBehaviour {

    public Sprite[] subSprites;

    private string slotName;

    public EquipmentSlot equipmentSlot;

    private Image image;
    private Image parentImage;

    private int characterID = 0;

    public int CharacterID
    {
        get { return characterID; }
        set
        {
            characterID = value;

            SetCharacter();
        }
    }

	// Use this for initialization
    void Start()
    {
        slotName = transform.name;
        image = GetComponent<Image>();
        parentImage = this.transform.parent.GetComponent<Image>();

        SetCharacter();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
	    if (equipmentSlot != null)
        {
            subSprites = equipmentSlot.subSprites;
            Sprite newSprite = null;

            if (subSprites != null && subSprites.Length > 0)
            {
                newSprite = Array.Find(subSprites, spr => spr.name == parentImage.sprite.name);
            }

            if (newSprite)
            {
                image.enabled = true;
                image.sprite = newSprite;
            }
            else
            {
                image.enabled = false;
            }
        }
	}

    private void SetCharacter()
    {
        //are we looking for player or npc?
        if (CharacterID == 0)
        {
            GameObject player = GameObject.Find("Player");

            if (player != null)
            {
                Transform childPart = player.transform.FindChild(slotName);

                if (childPart != null)
                {
                    equipmentSlot = childPart.GetComponent<EquipmentSlot>();
                }
            }
        }
        //TODO (Jukki) NPC previews? Party members
    }
}
