using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionsElement : MonoBehaviour {

    public GameObject actionPrefab;

    private List<GameObject> actions;
	// Use this for initialization
	void Start () {
        actions = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetDialog(Action actionDialog)
    {
        this.gameObject.SetActive(true);
        clearActions();

        foreach (KeyValuePair<string, int> dialog in actionDialog.Actions)
        {
            GameObject newAction = (GameObject)Instantiate(actionPrefab);

            newAction.name = "Actions";

            newAction.transform.SetParent(this.transform);

            ActionButton actionButton = newAction.GetComponent<ActionButton>();

            actionButton.SetButton(dialog.Value, dialog.Key);

            actions.Add(newAction);
        }
    }

    private void clearActions()
    {
        foreach (GameObject action in actions)
        {
            GameObject.Destroy(action);
        }
    }
}
