using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {


    /// <summary>
    /// Which team is the character on
    /// </summary>
    public FightSpot.FightTeam team
    {
        get;
        set;
    }

    /// <summary>
    /// Name of the character
    /// </summary>
    public string Name
    {
        get
        {
            NPCScript npc = GetComponent<NPCScript>();
            if (npc != null)
            {
                return npc.Npc.Name;
            }
            else
            {
                return "Player";
            }
        }
    }

    /// <summary>
    /// Attributes of the character
    /// </summary>
    public Atributes Atributes
    {
        get
        {
            return GetComponent<Atributes>();
        }
    }

    /// <summary>
    /// Currently selected fighter
    /// </summary>
    private static Fighter selected;
    public static Fighter Selected
    {
        set
        {
            if (selected == value)
            {
                selected.GetComponent<Renderer>().material.color = Color.white;
                selected = null;
                return;
            }
            if (selected != null)
            {
                selected.GetComponent<Renderer>().material.color = Color.white;
            }

            selected = value;

            if (selected != null)
            {
                selected.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
        get
        {
            return selected;
        }
    }


    public int Health = 50;
    public int Points = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {

        if (FightManager.InFight)
        {
            if (Selected != this)
            {
                GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    void OnMouseExit()
    {
        if (FightManager.InFight)
        {
            if (Selected != this)
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    void OnMouseDown()
    {
        if (FightManager.InFight)
        {
            if (Selected == this)
            {
                Selected = null;
                FightManager.HideOptions();
            }
            else
            {
                if (Selected == null)
                {
                    Selected = this;
                    FightManager.ShowOptions(Selected);
                }
            }
        }
    }
}
