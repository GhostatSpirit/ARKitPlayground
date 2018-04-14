using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeButtonManager))]
public class CubeButtonManagerEditor : Editor {

    public override void OnInspectorGUI()
    {
        CubeButtonManager manager = (CubeButtonManager)target;
        DrawDefaultInspector();

        if(GUILayout.Button("Generate Buttons"))
        {
            manager.GenerateButtons();
        }
    }
}
