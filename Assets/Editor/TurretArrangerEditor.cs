using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretArranger))]
public class TurretArrangerEditor : Editor {

    public override void OnInspectorGUI()
    {
        TurretArranger arranger = (TurretArranger)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Rearrange"))
        {
            arranger.rearrangeAllTurret();
        }

    }
}
