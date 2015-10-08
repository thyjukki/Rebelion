using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    static public GameObject player;
    static public GameObject Player
    {
        get
        {
            if (PlayerCharacter.player == null)
            {
                PlayerCharacter.player = GameObject.FindObjectOfType<PlayerCharacter>().gameObject;
            }

            return PlayerCharacter.player;
        }
    }

    SpriteRenderer pantsAnim;

    public float speed = 1.0f;

    public Inventory inventory;

    public CharacterPanel characterPanel;

    private Inventory chest;

    private List<GameObject> touching;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        inventory.AddItem(ItemScript.CreateItem(Category.Consumable, 0));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 1));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 2));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 3));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 4));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 5));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 6));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 7));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 8));
        inventory.AddItem(ItemScript.CreateItem(Category.Equipment, 9));

        touching = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        ParseMovement();

        if (Input.GetButtonDown("Inventory"))
        {
            Menu.Instance.OpenMenu(MenuTab.Inventory);
        }
        if (Input.GetButtonDown("Menu"))
        {
            Menu.ToggleMenu();
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (chest != null)
            {
                chest.Open();
            }
        }*/

        if (touching.Count > 0)
        {
            foreach (GameObject other in touching)
            {
                if (other.tag == "CharNPC")
                {

                    NPCScript npc = other.gameObject.GetComponent<NPCScript>();
                    if (npc.HasDialog() && !DialogManager.Instance.IsOpen)
                    {
                        if (Input.GetButtonDown("Talk"))
                        {
                            DialogManager.Instance.StartDialog(npc);
                            ObjectText.RemoveTarget();
                        }
                        else
                        {
                            ObjectText.SetTarget(other.gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            ObjectText.RemoveTarget();
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

        if (other.tag == "CharNPC")
        {
            if (!touching.Contains(other.gameObject))
            {
                touching.Add(other.gameObject);
            }
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

        if (other.tag == "CharNPC")
        {
            touching.Remove(other.gameObject);
        }
    }
}
