using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC
{
    public int ID { set; get; }
    public bool Female { set; get; }
    public string Skin { set; get; }
    public string Ears { set; get; }
    public string Nose { set; get; }
    public string EyeColor { set; get; }
    public int DialogID { set; get; }

    //public struct

    public List<KeyValuePair<string, int>> Equipments;

    public NPC()
    {
    }

    public NPC(int id, bool female, string skin, string ears, string nose, string eyecolor
        , List<KeyValuePair<string, int>> equipments, int dialogID)
    {
        this.ID = id;
        this.Female = female;
        this.Skin = skin;
        this.Ears = ears;
        this.Nose = nose;
        this.EyeColor = eyecolor;
        this.Equipments = equipments;
        this.DialogID = dialogID;
    }
}
