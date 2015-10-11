﻿using UnityEngine;
using System.Collections;

public enum MyEnum
{
    
}
public class Attack
{
    public int ID { get; set; }
    public int Need { get; set; }
    public bool Unarmed { get; set; }
    public bool Armed1H { get; set; }
    public bool Armed2H { get; set; }
    public bool ArmedBow { get; set; }
    public int MinLevel { get; set; }
    public CharacterClass Class { get; set; }
    public int MinAttack { get; set; }
    public int MaxAttack { get; set; }
    public int Accuracy { get; set; }
    public int Ammount { get; set; }
    public string Icon { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Attack()
    {
    }
}
