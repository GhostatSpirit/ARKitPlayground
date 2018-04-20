using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BastionSaver))]
public class BastionSaverEditor : Editor
{

    public override void OnInspectorGUI()
    {
        BastionSaver saver = (BastionSaver)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Save Bastion"))
        {
            saver.InitCubeIdDict();
            saver.Save();
        }

        if(GUILayout.Button("Load Bastion"))
        {
            saver.InitCubeIdDict();
            saver.Load();
        }
    }

}
