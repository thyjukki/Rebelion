﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    SpriteRenderer pantsAnim;

    public float speed = 1.0f;

    public Inventory inventory;

    private Inventory chest;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //pantsAnim = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        ParseMovement();

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.Open();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ItemScript itemScript = ItemScript.CreateItem(Category.Consumable, 0);
            inventory.AddItem(itemScript);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemScript itemScript = ItemScript.CreateItem(Category.Equipment, 0);
            inventory.AddItem(itemScript);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (chest != null)
            {
                chest.Open();
            }
        }
    }

    private void ParseMovement()
    {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("inputX", movement_vector.x);
            anim.SetFloat("inputY", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        rbody.MovePosition(rbody.position + movement_vector * speed * Time.deltaTime);
	
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            inventory.AddItem(other.GetComponent<ItemScript>());
        }

        if (other.tag == "Chest")
        {
            chest = other.GetComponent<ChestScript>().chestInventory;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Chest")
        {
            if (chest.IsOpen)
            {
                chest.Open();
            }
            chest = null;
        }
    }
}
