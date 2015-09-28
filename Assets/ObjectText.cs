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

    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        text.transform.position = target.transform.position;
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
