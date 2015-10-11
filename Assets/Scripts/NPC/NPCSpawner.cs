using UnityEngine;
using System.Collections;

public class NPCSpawner : MonoBehaviour {

    public bool onStart = true;

    public int npcID;
	// Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
	    if (onStart)
        {
            NPCManager.SpawnNPC(npcID, transform);
        }

	}
}
