using UnityEngine;
using System.Collections;

public class Fighter : MonoBehaviour {


    public FightSpot.FightTeam team
    {
        get;
        set;
    }

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

    private static Fighter selected;
    public static Fighter Selected
    {
        set
        {
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
            }
            else
            {
                if (Selected == null)
                {
                    Selected = this;
                }
                else
                {
                    FightManager.ShowOptions(Selected, this);
                }
            }
        }
    }
}
