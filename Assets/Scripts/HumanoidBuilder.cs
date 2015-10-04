using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HumanoidBuilder : MonoBehaviour {



    public bool female = false;

    public string Skin = "";

    public string Eyes = "";


    public string Ears;
    public string Nose;

    public CharacterPart EarObject;
    public CharacterPart EyesObject;
    public CharacterPart NoseObject;

    public Sprite[] subCharSprites;
    private SpriteRenderer charRenderer;

    public bool Female
    {
        get { return female; }
        set
        {
            female = value;
            SetBodyParts();
            SetBody();
        }
    }
    public string SkinName
    {
        get { return Skin; }
        set
        {
            Skin = value;
            SetBody();
        }
    }
    public string EyeName
    {
        get { return Eyes; }
        set
        {
            Eyes = value;
            string path; // Check if the character is female and then get path names
            if (Female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (EyesObject != null && Eyes != String.Empty)
            {
                EyesObject.SetSprite(path + "Eyes/" + Eyes);
            }
        }
    }
    public string EarType
    {
        get { return Ears; }
        set
        {
            Ears = value;
            string path; // Check if the character is female and then get path names
            if (Female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (EarObject != null)
            {
                if (Ears == string.Empty) // Check what type of ears the character has
                    EarObject.DisableSprite();
                else
                {
                    string earPath = path + "Ears/" + Ears + "_" + Skin;

                    EarObject.SetSprite(earPath);
                }
            }
        }
    }
    public string NoseType
    {
        get { return Nose; }
        set
        {
            Nose = value;
            string path; // Check if the character is female and then get path names
            if (Female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (NoseObject != null)
            {
                if (Nose == string.Empty) // Check what type of ears the character has
                    NoseObject.DisableSprite();
                else
                {
                    string nosePath = path + "Nose/" + Nose + "bignose_" + Skin;

                    NoseObject.SetSprite(nosePath);
                }
            }
        }
    }

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

    virtual protected void SetBody()
    {
        string path; // Check if the character is female and then get path names
        if (Female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        subCharSprites = Resources.LoadAll<Sprite>(path + Skin); // Find sub Sprites

        charRenderer = GetComponent<SpriteRenderer>(); // Get all sprite renderer
    }

    private void SetBodyParts()
    {
        string path; // Check if the character is female and then get path names
        if (Female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        if (EarObject != null)
        {
            if (Ears == string.Empty) // Check what type of ears the character has
                EarObject.DisableSprite();
            else
            {
                string earPath = path + "Ears/" + Ears + "_" + Skin;

                EarObject.SetSprite(earPath);
            }
        }

        if (NoseObject != null)
        {
            if (Nose == string.Empty) // Check what type of ears the character has
                NoseObject.DisableSprite();
            else
            {
                string nosePath = path + "Nose/" + Nose + "bignose_" + Skin;

                NoseObject.SetSprite(nosePath);
            }
        }

        if (EyesObject != null && Eyes != String.Empty)
        {
            EyesObject.SetSprite(path + "Eyes/" + Eyes);
        }
    }
}
