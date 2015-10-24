using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SpriteCopy : EditorWindow
{

    Object copyFrom;
    //Object copyTo;
    int count;
    public Object[] CopyTo = { };

    // Creates a new option in "Windows"
    [MenuItem("Window/Copy Spritesheet pivots and slices")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SpriteCopy window = (SpriteCopy)EditorWindow.GetWindow(typeof(SpriteCopy));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Copy from:", EditorStyles.boldLabel);
        copyFrom = EditorGUILayout.ObjectField(copyFrom, typeof(Texture2D), false, GUILayout.Width(220));
        GUILayout.EndHorizontal();

        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty objectsProperty = so.FindProperty("CopyTo");

        EditorGUILayout.PropertyField(objectsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties

        GUILayout.Space(25f);
        if (GUILayout.Button("Copy pivots and slices"))
        {
            CopyPivotsAndSlices();
        }
    }

    void CopyPivotsAndSlices()
    {
        if (!copyFrom || CopyTo.Length == 0)
        {
            Debug.Log("Missing one object");
            return;
        }

        if (copyFrom.GetType() != typeof(Texture2D))
        {
            Debug.Log("Cant convert from: " + copyFrom.GetType()+ ". Needs two Texture2D objects!");
            return;
        }
        string copyFromPath = AssetDatabase.GetAssetPath(copyFrom);
        TextureImporter ti1 = AssetImporter.GetAtPath(copyFromPath) as TextureImporter;
        ti1.isReadable = true;
        Debug.Log("Amount of slices found: " + ti1.spritesheet.Length);

        List<SpriteMetaData> newData = new List<SpriteMetaData>();


        for (int i = 0; i < ti1.spritesheet.Length; i++)
        {
            SpriteMetaData d = ti1.spritesheet[i];
            newData.Add(d);
        }

        foreach (Object copyTo in CopyTo)
        {
            if (copyTo.GetType() != typeof(Texture2D))
            {
                Debug.Log("Cant convert to: " + copyTo.GetType() + ". Needs two Texture2D objects!");
                return;
            }

            string copyToPath = AssetDatabase.GetAssetPath(copyTo);
            TextureImporter ti2 = AssetImporter.GetAtPath(copyToPath) as TextureImporter;
            ti2.isReadable = true;

            ti2.spriteImportMode = SpriteImportMode.Multiple;
            ti2.spritesheet = newData.ToArray();

            AssetDatabase.ImportAsset(copyToPath, ImportAssetOptions.ForceUpdate);

            ti2.filterMode = ti1.filterMode;
        }
    }
}