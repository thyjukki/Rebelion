using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;

    public Text nameText;

    public CanvasGroup canvasGroup;

    private static DialogManager instance;

    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
            }

            return DialogManager.instance;
        }
    }

    private bool isOpen = false;
    public bool IsOpen
    {
        get { return isOpen; }
        private set
        {
            isOpen = value;
        }
    }

    private DialogContainer dialogContainer;

	// Use this for initialization
	void Start () {
        Type[] dialogTypes = { typeof(Dialog)};

        XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer), dialogTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Dialogs.xml");

        dialogContainer = (DialogContainer)serializer.Deserialize(textReader);
        textReader.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartDialog(NPCScript npcScript)
    {
        int dialogID = npcScript.Npc.DialogID;

        Dialog dialog = dialogContainer.FindDialog(dialogID);

        nameText.text = npcScript.Npc.Name;
        canvasGroup.alpha = 1;
        IsOpen = true;
    }
}
