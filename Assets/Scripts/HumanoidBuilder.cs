using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HumanoidBuilder : MonoBehaviour {


    public enum Ears { Normal, Big, Elf };
    public enum Nose { Normal, BigNose, ButtonNose, StraightNose };

    public bool female = false;
    public string skinName = "";
    public string eyeName = "";

    public Ears EarType;
    public Nose NoseType;

    public CharacterPart EarObject;
    public CharacterPart EyesObject;
    public CharacterPart NoseObject;

    public Sprite[] subCharSprites;
    private SpriteRenderer charRenderer;

    void Start()
    {
        SetBodyParts();
        SetBody();
    }

    void LateUpdate()
    {
        // Find replacement sprites
        Sprite newCharSprite = Array.Find(subCharSprites, item => item.name == charRenderer.sprite.name);

        if (newCharSprite) // If replacement sprites were found, replace them
            charRenderer.sprite = newCharSprite;
	}

    void SetBody()
    {
        string path; // Check if the character is female and then get path names
        if (female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        subCharSprites = Resources.LoadAll<Sprite>(path + skinName); // Find sub Sprites

        charRenderer = GetComponent<SpriteRenderer>(); // Get all sprite renderer
    }

    void SetBodyParts()
    {
        string path; // Check if the character is female and then get path names
        if (female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        if (EarObject != null)
        {
            if (EarType == Ears.Normal) // Check what type of ears the character has
                EarObject.DisableSprite();
            else
            {
                string earPath = path + "Ears/";
                if (EarType == Ears.Big)
                    earPath = earPath + "bigears_" + skinName;
                else if (EarType == Ears.Elf)
                    earPath = earPath + "elvenears_" + skinName;

                EarObject.SetSprite(earPath);
            }
        }

        if (NoseObject != null)
        {
            if (NoseType == Nose.Normal) // Check what type of ears the character has
                NoseObject.DisableSprite();
            else
            {
                string nosePath = path + "Nose/";
                if (NoseType == Nose.BigNose)
                    nosePath = nosePath + "bignose_" + skinName;
                else if (NoseType == Nose.ButtonNose)
                    nosePath = nosePath + "buttonnose_" + skinName;
                else if (NoseType == Nose.StraightNose)
                    nosePath = nosePath + "straightnose_" + skinName;

                NoseObject.SetSprite(nosePath);
            }
        }

        if (EyesObject != null && eyeName != String.Empty)
        {
            EyesObject.SetSprite(path + "Eyes/" + eyeName);
        }
    }
}
