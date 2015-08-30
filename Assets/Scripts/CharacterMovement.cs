using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    SpriteRenderer pantsAnim;

    public float speed = 1.0f;

    public Inventory inventory;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //pantsAnim = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
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
            inventory.AddItem(other.GetComponent<Item>());
        }
    }
}
