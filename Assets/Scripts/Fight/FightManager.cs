using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

public class FightManager : MonoBehaviour {

    private static FightManager instance;

    public static FightManager Instance
    {
        get
        {
            if (FightManager.instance == null)
            {
                FightManager.instance = GameObject.FindObjectOfType<FightManager>();
            }

            return FightManager.instance;
        }
    }

    public FightContainer Fights { get; private set; }


    /// <summary>
    /// Load all fights in XML by using this.
    /// </summary>
    public void Awake()
    {
        Type[] itemTypes = { typeof(Fight) };

        XmlSerializer serializer = new XmlSerializer(typeof(FightContainer), itemTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Fights.xml");

        Fights = (FightContainer)serializer.Deserialize(textReader);
        textReader.Close();

        DontDestroyOnLoad(gameObject);

    }

	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartFight(int id)
    {
        Fight fight = Fights.FindFight(id);

        if (fight == null)
        {
            Debug.Log("Unknown fight id " + id.ToString());
            return;
        }

        Application.LoadLevelAdditive(1);
    }
}
