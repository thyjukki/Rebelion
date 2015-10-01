using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public DialogElement dialogElement;

    public ActionsElement actionsElement;

    public Text nameText;

    public CanvasGroup canvasGroup;

    private static DialogManager instance;

    private GameObject currentActor;

    private Dialog currentDialog;
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

    public bool IsOpen{ get; private set; }

    private DialogContainer dialogContainer;

	// Use this for initialization
	void Start () {
        Type[] dialogTypes = { typeof(Dialog), typeof(TextDialog), typeof(Action) };

        XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer), dialogTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/dialogs.xml");

        dialogContainer = (DialogContainer)serializer.Deserialize(textReader);
        textReader.Close();

        dialogElement.gameObject.SetActive(false);
        actionsElement.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void StartDialog(NPCScript npcScript)
    {
        int dialogID = npcScript.Npc.DialogID;
        SetCurrentDialog(dialogID);

        currentDialog.Start();

        canvasGroup.alpha = 1;
        IsOpen = true;
    }
    public void StartDialog(int dialogID)
    {
        SetCurrentDialog(dialogID);

        if (currentDialog != null)
        {
            currentDialog.Start();
            canvasGroup.alpha = 1;
            IsOpen = true;
        }

    }

    private void SetActor(int actorID)
    {
        if (actorID == 0)
        {
            nameText.text = GameObject.Find("Player").name;
        }
        else
        {
            NPC npc = NPCManager.GetNPC(actorID);

            if (npc == null)
            {
                Debug.LogError("Null NPC " + actorID.ToString());
                return;
            }

            nameText.text = npc.Name;
        }
    }

    private void SetCurrentDialog(int dialogID)
    {
        if (dialogID == -1)
        {
            dialogElement.gameObject.SetActive(false);
            actionsElement.gameObject.SetActive(false);
            currentDialog = null;
            IsOpen = false;
            return;
        }
        currentDialog = dialogContainer.FindDialog(dialogID);

        if (currentDialog == null)
        {
            Debug.LogError("Null Dialog " + dialogID.ToString());
            return;
        }

        SetActor(currentDialog.ActorID); 
    }


    public void Advance(int dialogID)
    {
        SetCurrentDialog(dialogID);

        if (currentDialog != null)
        {
            currentDialog.Start();
        }
    }
}
