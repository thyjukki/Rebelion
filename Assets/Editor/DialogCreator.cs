using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;

public class DialogCreator : EditorWindow
{

    string dialogText;
    int actorID = 0;
    int nextID = 0;

    static int currentId = 1;

    [MenuItem("Window/Create an dialog")]
    static void Init()
    {
        GetIDCount();
        // Get existing open window or if none, make a new one:
        DialogCreator window = (DialogCreator)EditorWindow.GetWindow(typeof(DialogCreator));
        window.Show();
    }


    /// <summary>
    /// TODO(Jukki) Auto select id
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Dialog ID:", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(currentId.ToString(), EditorStyles.boldLabel, GUILayout.Width(220));
        //currentId = EditorGUILayout.IntField(currentId, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Dialog Text:", EditorStyles.boldLabel);
        dialogText = EditorGUILayout.TextArea(dialogText, GUILayout.Width(220), GUILayout.Height(60));
        GUILayout.EndHorizontal();

        GUILayout.Space(25f);
        if (GUILayout.Button("Create Dialog"))
        {
            CreateDialog();
            GetIDCount();
        }
        if (GUILayout.Button("Refresh ID"))
        {
            GetIDCount();
        }
    }

    public void CreateDialog()
    {
        GetIDCount();
        DialogContainer dialogContainer = new DialogContainer();
        Type[] dialogTypes = { typeof(Dialog) };

        XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer), dialogTypes);
        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "dialogs.xml"), FileMode.Open);

        dialogContainer = (DialogContainer)serializer.Deserialize(fs);
        fs.Close();

        dialogContainer.Dialogs.Add(new TextDialog(currentId, nextID ,actorID, name, dialogText));

        fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "dialogs.xml"), FileMode.Create);
        serializer.Serialize(fs, dialogContainer);
        fs.Close();
    }

    static void GetIDCount()
    {
        DialogContainer dialogContainer = new DialogContainer();
        Type[] dialogTypes = { typeof(Dialog) };

        XmlSerializer serializer = new XmlSerializer(typeof(DialogContainer), dialogTypes);
        FileStream fs = new FileStream(Path.Combine(Application.streamingAssetsPath, "dialogs.xml"), FileMode.Open);

        dialogContainer = (DialogContainer)serializer.Deserialize(fs);
        fs.Close();


        int minId = 0;
        foreach (Dialog dialog in dialogContainer.Dialogs)
        {
            if (dialog.ID == minId)
            {
                minId = dialog.ID + 1;
            }
        }

        currentId = minId;
    }
}
