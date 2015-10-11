using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


    private GameObject target;
    public float scale = 4f;
    public float speed = 1.0f;
    Camera myCam;

	// Use this for initialization
	void Start () {

        myCam = GetComponent<Camera>();

        target = PlayerCharacter.Player;

        if (target)
        {
            transform.position = target.transform.position;
        }
	}
	
	// Update is called once per frame
    void LateUpdate()
    {

        myCam.orthographicSize = (Screen.height / 100f) / scale;

        if (target) {

            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed) - new Vector3 (0,0,10);
        }
	}
}
