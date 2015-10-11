using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    private NPCContainer npcContainer = new NPCContainer();

    public NPCContainer NpcContainer
    {
        get { return npcContainer; }
        set { npcContainer = value; }
    }

    private List<GameObject> npcs; 
    public List<GameObject> Npcs
    {
        private set
        {
            npcs = value;
        }
        get
        {
            if (npcs == null)
            {
                npcs = new List<GameObject>();
            }

            return npcs;
        }
    }

    private static NPCManager instance;

    public static NPCManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NPCManager>();
            }

            return NPCManager.instance;
        }
    }

    public GameObject npcPrefab;

    // Use this for initialization
    void Awake()
    {
        Type[] npcTypes = { typeof(NPC) };

        XmlSerializer serializer = new XmlSerializer(typeof(NPCContainer), npcTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Npcs.xml");

        npcContainer = (NPCContainer)serializer.Deserialize(textReader);

        textReader.Close();

        /*KeyValuePair<string, int> test = new KeyValuePair<string, int>("Legs", 0);
        var list = new List<KeyValuePair<string, int>>();
        list.Add(test);

        NPC npc = new NPC(0, false, "light", string.Empty, string.Empty, string.Empty, list);
        
        npcContainer.Npcs.Add(npc);

        System.Type[] itemTypes = { typeof(Equipment), typeof(Weapon), typeof(Consumable) };


        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "Npcs.xml"), FileMode.Create);
        serializer.Serialize(fs, npcContainer);
        fs.Close();*/
    }

    public static NPC GetNPC(int id)
    {
        return Instance.NpcContainer.Npcs.Find(x => x.ID == id);
    }

    public static GameObject SpawnNPC(int npcID, Transform newTransform)
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

            newNPC.transform.position = newTransform.position;

            newNPC.transform.parent = instance.transform;
            newNPC.name = "NPC_" + npc.Name;

            instance.Npcs.Add(newNPC);
            return newNPC;
        }

        return null;
    }
}
