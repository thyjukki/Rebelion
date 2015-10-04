using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Stats))]
public class StatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Stats myStats = (Stats)target;

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
            myStats.GiveExperience();
        }
    }
}
