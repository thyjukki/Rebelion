using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;
using System.IO;

public class DialogManager : MonoBehaviour {

    private static DialogManager instance;

    //private DialogContainer dialogContainer = new DialogContainer();

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

	// Use this for initialization
	void Start () {
        Type[] dialogTypes = { typeof(Dialog)};

        XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer), dialogTypes);
        TextReader textReader = new StreamReader(Application.streamingAssetsPath + "/Dialogs.xml");

        //dialogContainer = (DialogContainer)serializer.Deserialize(textReader);
        textReader.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
