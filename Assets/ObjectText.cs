using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectText : MonoBehaviour {

    private GameObject target;
    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;

            if (target == null)
            {
                RemoveTarget();
            }
            else
            {
                SetTarget(target);
            }
        }
    }

    private static ObjectText instance;

    public static ObjectText Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectText>();
            }
            return ObjectText.instance;
        }
    }

    public float YOffset;

    private Vector3 offset;

    private Text text;
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        offset = new Vector3(0F, YOffset, 0F);
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (target != null)
        {
            text.transform.position = target.transform.position + offset;
        }
	}

    public static void SetTarget(GameObject tg)
    {
        Instance.text.enabled = true;
        Instance.target = tg;
    }

    public static void RemoveTarget()
    {
        Instance.text.enabled = false;
        Instance.target = null;
    }
}
