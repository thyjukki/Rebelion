using UnityEngine;
using System.Collections;

public class FightSpot : MonoBehaviour {


    public enum FaceDir
    {
        Left,
        Right
    }

    public enum FightTeam
    {
        Good,
        Bad
    }

    public FightTeam Team;

    public FaceDir Direction;

    public int ID;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
