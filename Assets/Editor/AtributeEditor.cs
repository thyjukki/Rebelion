using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Atributes))]
public class AtributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Atributes myStats = (Atributes)target;

        DrawDefaultInspector();

        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Experience", myStats.Experience.ToString());
        EditorGUILayout.LabelField("Level", myStats.Level.ToString());

        EditorGUILayout.Separator();

        if (GUILayout.Button("Clear Experience"))
        {
            myStats.ClearExperience();
        }

        if (GUILayout.Button("Give Experience"))
        {
            myStats.GiveExperience(750);
        }
    }
}
