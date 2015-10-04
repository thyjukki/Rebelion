using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterPreview : HumanoidBuilder
{
    public HumanoidBuilder humanoidBuilder;

    public HumanoidBuilder HumanoidBuilder
    {
        get
        {
            return humanoidBuilder;
        }

        set
        {
            humanoidBuilder = value;
            SetCharacter();
        }
    }

    private Image charImage;

    void Start()
    {
        if (HumanoidBuilder != null)
        {
            SetCharacter();
        }
    }

    void LateUpdate()
    {
        // Find replacement sprites
        Sprite newCharSprite = Array.Find(subCharSprites, item => item.name == charImage.sprite.name);

        if (newCharSprite) // If replacement sprites were found, replace them
            charImage.sprite = newCharSprite;
    }

    override protected void SetBody()
    {
        string path; // Check if the character is female and then get path names
        if (Female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        subCharSprites = Resources.LoadAll<Sprite>(path + Skin); // Find sub Sprites

        charImage = GetComponent<Image>(); // Get all sprite renderer
    }

    private void SetCharacter()
    {
        this.Female = humanoidBuilder.Female;
        this.Eyes = humanoidBuilder.Eyes;
        this.Skin = humanoidBuilder.Skin;
        this.Ears = humanoidBuilder.Ears;
    }
}
