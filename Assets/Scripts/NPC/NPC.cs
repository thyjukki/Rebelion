﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC
{
    public int ID { set; get; }
    public string Name { set; get; }
    public bool Female { set; get; }
    public bool Unique { set; get; }
    public string Skin { set; get; }
    public string Ears { set; get; }
    public string Nose { set; get; }
    public string EyeColor { set; get; }
    public int DialogID { set; get; }
    public List<KeyValuePair<string, int>> Equipments { set; get; }

    //public struct


    public NPC()
    {
    }

    public NPC(int id, string name, bool female, bool unique, string skin, string ears, string nose, string eyecolor
        , List<KeyValuePair<string, int>> equipments, int dialogID)
    {
        this.ID = id;
        this.Name = name;
        this.Female = female;
        this.Unique = unique;
        this.Skin = skin;
        this.Ears = ears;
        this.Nose = nose;
        this.EyeColor = eyecolor;
        this.Equipments = equipments;
        this.DialogID = dialogID;
    }
}
