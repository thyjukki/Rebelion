using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterPanel : Inventory
{

    public Slot[] equipmentSlots;

    void Awake()
    {
        equipmentSlots = transform.GetComponentsInChildren<Slot>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
