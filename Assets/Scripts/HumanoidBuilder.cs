using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HumanoidBuilder : MonoBehaviour {



    public bool female = false;

    public string skinName = "";

    public string eyeName = "";


    public string earType;
    public string noseType;


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
        get { return skinName; }
        set
        {
            skinName = value;
            SetBody();
        }
    }
    public string EyeName
    {
        get { return eyeName; }
        set
        {
            eyeName = value;
            string path; // Check if the character is female and then get path names
            if (female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (EyesObject != null && eyeName != String.Empty)
            {
                EyesObject.SetSprite(path + "Eyes/" + eyeName);
            }
        }
    }
    public string EarType
    {
        get { return earType; }
        set
        {
            earType = value;
            string path; // Check if the character is female and then get path names
            if (female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (EarObject != null)
            {
                if (earType == string.Empty) // Check what type of ears the character has
                    EarObject.DisableSprite();
                else
                {
                    string earPath = path + "Ears/" + earType + "_" + skinName;

                    EarObject.SetSprite(earPath);
                }
            }
        }
    }
    public string NoseType
    {
        get { return noseType; }
        set
        {
            noseType = value;
            string path; // Check if the character is female and then get path names
            if (female)
                path = "Characters/Female/";
            else
                path = "Characters/Male/";

            if (NoseObject != null)
            {
                if (noseType == string.Empty) // Check what type of ears the character has
                    NoseObject.DisableSprite();
                else
                {
                    string nosePath = path + "Nose/" + noseType + "bignose_" + skinName;

                    NoseObject.SetSprite(nosePath);
                }
            }
        }
    }

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
            if (earType == string.Empty) // Check what type of ears the character has
                EarObject.DisableSprite();
            else
            {
                string earPath = path + "Ears/" + earType + "_" + skinName;

                EarObject.SetSprite(earPath);
            }
        }

        if (NoseObject != null)
        {
            if (noseType == string.Empty) // Check what type of ears the character has
                NoseObject.DisableSprite();
            else
            {
                string nosePath = path + "Nose/" + noseType + "bignose_" + skinName;

                NoseObject.SetSprite(nosePath);
            }
        }

        if (EyesObject != null && eyeName != String.Empty)
        {
            EyesObject.SetSprite(path + "Eyes/" + eyeName);
        }
    }
}
