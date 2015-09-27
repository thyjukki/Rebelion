using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCContainer {
    private List<NPC> npcs= new List<NPC>();

    public List<NPC> Npcs
    {
        get { return npcs; }
        set { npcs = value; }
    }
}
