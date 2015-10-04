using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CharacterPreviewPart : CharacterPart
{

    private Image image;

    private Image parentImage;

    // Use this for initialization
    void Awake()
    {
        image = GetComponent<Image>();
        parentImage = this.transform.parent.GetComponent<Image>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (image.enabled)
        {
            Sprite newSprite = Array.Find(subSprites, spr => spr.name == parentImage.sprite.name);

            if (newSprite)
                image.sprite = newSprite;
        }

    }

    override public void DisableSprite()
    {
        image.enabled = false;
        subSprites = null;
    }

    override public void SetSprite(string path)
    {

        subSprites = Resources.LoadAll<Sprite>(path);

        if (subSprites.Length > 0)
        {
            image.enabled = true;
        }
    }
}
