using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightContainer
{
    private List<Fight> fights = new List<Fight>();

    public List<Fight> Fights
    {
        get { return fights; }
        set { fights = value; }
    }

    public Fight FindFight(int id)
    {
        return Fights.Find(x => x.ID == id);
    }
}
