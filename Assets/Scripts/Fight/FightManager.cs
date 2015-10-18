using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public enum FightState
{
    None,
    Begining,
    Chosing,
    Processing,
    Ending
}

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
    public FeatContainer Feats { get; private set; }

    public GameObject SharedObjects
    {
        get
        {
            return GameObject.Find("SharedObjects");
        }
    }
    public GameObject Managers
    {
        get
        {
            return GameObject.Find("Managers");
        }
    }
    public GameObject OverworldObjects
    {
        get
        {
            return GameObject.Find("OverworldObjects");
        }
    }
    public GameObject FightObjects
    {
        get
        {
            return GameObject.Find("FightObjects");
        }
    }

    public static bool InFight
    {
        get;
        private set;
    }

    public Fight currentFight { get; private set; }

    private FightState state;

    public FightState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }


    private List<Fighter> fighters;

    //Inspector elements
    public GameObject NameTextPrefab;

    public float fadeTime;

    public Image Overlay;

    public FightCharacterBar CharacterBar;

    public FightOptions Options;

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

        Type[] featTypes = { typeof(Attack) };

        serializer = new XmlSerializer(typeof(FeatContainer), featTypes);
        textReader = new StreamReader(Application.streamingAssetsPath + "/Feats.xml");

        Feats = (FeatContainer)serializer.Deserialize(textReader);
        textReader.Close();

        Overlay.rectTransform.rect.Set(0, 0, Screen.width, Screen.height);
    }

    void Start()
    {
        GameObject.DontDestroyOnLoad(Managers);
        GameObject.DontDestroyOnLoad(SharedObjects);
    }
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnLevelWasLoaded(int level)
    {
        this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (level == 1)
        {
            Destroy(OverworldObjects);
            StartCoroutine(FadeToClear());
            SetFighters();
            State = FightState.Begining;
        }
        else if (level == 0)
        {
            Destroy(FightObjects);
        }
    }


    /// <summary>
    /// Set the fighters to their positions.
    /// </summary>
    private void SetFighters()
    {
        List<FightSpot> allSpots = new List<FightSpot>(GameObject.FindObjectsOfType<FightSpot>());
        List<FightSpot> friendlySpots = allSpots.FindAll(x => x.Team == FightSpot.FightTeam.Good);
        List<FightSpot> enemySpots = allSpots.FindAll(x => x.Team == FightSpot.FightTeam.Bad);

        friendlySpots.Sort(delegate(FightSpot x, FightSpot y)
        {
            return x.ID.CompareTo(y.ID);
        });
        enemySpots.Sort(delegate(FightSpot x, FightSpot y)
        {
            return x.ID.CompareTo(y.ID);
        });
        
        // First set players location
        PlayerCharacter.Player.transform.position = friendlySpots[0].transform.position;
        PlayerCharacter.Player.GetComponent<Animator>().SetBool("isWalking", false);
        PlayerCharacter.Player.GetComponent<Animator>().SetFloat("inputX", 1f);
        PlayerCharacter.Player.GetComponent<Animator>().SetFloat("inputY", 0f);

        fighters = new List<Fighter>();
        fighters.Add(PlayerCharacter.Player.GetComponent<Fighter>());

        int i = 0;
        foreach (int enemyID in currentFight.Enemies)
        {
            NPC enemy = NPCManager.GetNPC(enemyID);

            if (enemy.Unique)
            {
                GameObject overworldNPC = NPCManager.Instance.Npcs.Find(x => x.GetComponent<NPCScript>().Npc == enemy);

                overworldNPC.SetActive(true);

                overworldNPC.transform.position = enemySpots[i].transform.position;
                overworldNPC.GetComponent<Animator>().SetFloat("inputX", -1f);
                overworldNPC.GetComponent<Animator>().SetFloat("inputY", 0f);

                Fighter fighter = overworldNPC.GetComponent<Fighter>();
                fighter.team = FightSpot.FightTeam.Bad;
                fighters.Add(fighter);
            }
            //enemyNPCs.Add();

            i++;
        }
    }

    public void StartFight(int id)
    {
        Fight fight = Fights.FindFight(id);

        if (fight == null)
        {
            Debug.Log("Unknown fight id " + id.ToString());
            return;
        }
        StartCoroutine(FadeToBlack(fight));
    }

    private void BeginFight()
    {
        SetCharacterNames();
        CharacterBar.gameObject.SetActive(true);
        CharacterBar.SetCharacters(fighters.FindAll(x => x.team == FightSpot.FightTeam.Good));
    }


    public void SetCharacterNames()
    {
        foreach (Fighter fighter in fighters)
        {
            GameObject nameText = Instantiate(NameTextPrefab);

            nameText.name = "Name_" + fighter.Name;
            nameText.transform.SetParent(this.transform);
            nameText.GetComponent<Text>().text = fighter.Name;
            nameText.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            float yOffset = (fighter.GetComponent<Renderer>().bounds.size.y/2);
            Vector3 pos = fighter.transform.position +new Vector3(0f, -yOffset, 0f);

            nameText.transform.position = pos;
        }
    }


    public static void ShowOptions(Fighter Selected)
    {
        Instance.Options.SetState(OptionState.Main);
    }
    public static void HideOptions()
    {
        Instance.Options.SetState(OptionState.None);
    }

    /// <summary>
    /// Fade out and start the fighting
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeToClear()
    {
        Overlay.rectTransform.rect.Set(0, 0, Screen.width, Screen.height);
        Overlay.gameObject.SetActive(true);
        Overlay.color = Color.black;

        float rate = 1.0f / fadeTime;

        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Overlay.color = Color.Lerp(Color.black, Color.clear, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        Overlay.color = Color.clear;
        Overlay.gameObject.SetActive(false);
        State = FightState.Chosing;

        BeginFight();
    }

    /// <summary>
    /// Fades in to the given fight.
    /// </summary>
    /// <param name="fight">Fight that we are fading in to.</param>
    /// <returns></returns>
    private IEnumerator FadeToBlack(Fight fight)
    {
        Overlay.rectTransform.rect.Set(0, 0, Screen.width, Screen.height);
        Overlay.gameObject.SetActive(true);
        Overlay.color = Color.clear;

        float rate = 1.0f / fadeTime;

        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Overlay.color = Color.Lerp(Color.clear, Color.black, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }


        currentFight = fight;
        foreach (GameObject npc in NPCManager.Instance.Npcs)
        {
            npc.SetActive(false);
        }
        InFight = true;
        Application.LoadLevel(1);
        Overlay.color = Color.black;
    }
}
