﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FeatContainer {
    public List<Attack> Attacks;

    public FeatContainer()
    {

    }


    public Attack FindAttack(int id)
    {
        return Attacks.Find(x => x.ID == id);
    }
}
