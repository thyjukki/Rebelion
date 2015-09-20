using UnityEngine;
using System.Collections;
using System;

public class CharacterPart : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public Sprite[] subSprites;

    private SpriteRenderer parentSpriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = this.transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (spriteRenderer.enabled)
        {
            Sprite newSprite = Array.Find(subSprites, spr => spr.name == parentSpriteRenderer.sprite.name);

            if (newSprite)
                spriteRenderer.sprite = newSprite;
        }

    }

    public void DisableSprite()
    {
        spriteRenderer.enabled = false;
        subSprites = null;
    }

    public void SetSprite(string path)
    {

        subSprites = Resources.LoadAll<Sprite>(path);

        if (subSprites.Length > 0)
        {
            spriteRenderer.enabled = true;
        }
    }
}
