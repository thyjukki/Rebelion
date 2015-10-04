using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public int Strenght = 8;

    public int Dexterity = 8;

    public int Intelligent = 8;

    public int Magic = 8;


    public int Experience
    {
        get;
        private set;
    }

    public int Level
    {
        get;
        private set;
    }

    public bool CanLevelUp
    {
        get
        {
            if (expToLevel > Level)
                return true;
            else
                return false;
        }
    }

    private int expToLevel
    {
        get { return Experience / 750; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetExperience(int exp)
    {

    }


    public void ClearExperience()
    {
        Experience = 0;
        Level = 0;
    }

    public void GiveExperience()
    {
        Experience += 750;
    }
}
