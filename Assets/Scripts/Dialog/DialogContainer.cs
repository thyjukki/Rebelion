﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogContainer
{
    private List<Dialog> dialogs = new List<Dialog>();

    public List<Dialog> Dialogs
    {
        get { return dialogs; }
        set { dialogs = value; }
    }

}