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



    void start()
    {

    }

    void LateUpdate()
    {
        GameObject EarObject;
        GameObject PantsObject;
        GameObject EyesObject;
        GameObject NoseObject;

        EarObject = this.transform.FindChild("Ears").gameObject; // First get the child objects of character
        PantsObject = this.transform.FindChild("Pants").gameObject;
        EyesObject = this.transform.FindChild("Eyes").gameObject;
        NoseObject = this.transform.FindChild("Nose").gameObject;

        if (EarType == Ears.Normal) // Check what type of ears the character has
            EarObject.SetActive(false);
        else
            EarObject.SetActive(true);

        if (NoseType == Nose.Normal) // Check what type of ears the character has
            NoseObject.SetActive(false);
        else
            NoseObject.SetActive(true);


        string path; // Check if the character is female and then get path names
        if (female)
            path = "Characters/Female/";
        else
            path = "Characters/Male/";

        string earPath = path + "Ears/";
        if (EarType == Ears.Big)
            earPath = earPath + "bigears_" + skinName;
        else if (EarType == Ears.Elf)
            earPath = earPath + "elvenears_" + skinName;

        string nosePath = path + "Nose/";
        if (NoseType == Nose.BigNose)
            nosePath = nosePath + "bignose_" + skinName;
        else if (NoseType == Nose.ButtonNose)
            nosePath = nosePath + "buttonnose_" + skinName;
        else if (NoseType == Nose.StraightNose)
            nosePath = nosePath + "straightnose_" + skinName;

        Sprite[] subCharSprites = Resources.LoadAll<Sprite>(path + skinName); // Find sub Sprites
        Sprite[] subEarSprites = Resources.LoadAll<Sprite>(earPath);
        Sprite[] subEyeSprites = Resources.LoadAll<Sprite>(path + "Eyes/" + eyeName);
        Sprite[] subNoseSprites = Resources.LoadAll<Sprite>(nosePath);


        var charRenderer = GetComponent<SpriteRenderer>(); // Get all sprite renderers
        var earRenderer = EarObject.GetComponent<SpriteRenderer>();
        var eyesRenderer = EyesObject.GetComponent<SpriteRenderer>();
        var noseRenderer = NoseObject.GetComponent<SpriteRenderer>();

        string charSpriteName = charRenderer.sprite.name;
        string earSpriteName = earRenderer.sprite.name;
        string eyeSpriteName = eyesRenderer.sprite.name;
        string noseSpriteName = noseRenderer.sprite.name;

        var newCharSprite = Array.Find(subCharSprites, item => item.name == charSpriteName); // Find replacement sprites
        var newEarSprite = Array.Find(subEarSprites, item => item.name == earSpriteName);
        var newEyeSprite = Array.Find(subEyeSprites, item => item.name == eyeSpriteName);
        var newNoseSprite = Array.Find(subNoseSprites, item => item.name == noseSpriteName);

        if (newCharSprite) // If replacement sprites were found, replace them
            charRenderer.sprite = newCharSprite;

        if (newEarSprite)
            earRenderer.sprite = newEarSprite;

        if (newEyeSprite)
            eyesRenderer.sprite = newEyeSprite;

        if (newNoseSprite)
            noseRenderer.sprite = newNoseSprite;
	}
}
