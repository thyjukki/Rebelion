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
            SpawnNPC();
        }

	}

    void SpawnNPC()
    {

        NPC npc = NPCManager.GetNPC(npcID);

        if (npc == null)
        {
            Debug.Log("Missing npc with id " + npcID.ToString());
        }
        else
        {
            GameObject newNPC = (GameObject)Instantiate(NPCManager.Instance.npcPrefab);

            newNPC.GetComponent<NPCScript>().Npc = npc;

            newNPC.transform.position = this.transform.position;

        }
    }
}
