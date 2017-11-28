using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {

	protected virtual void OnSceneGUI()
	{
		if (Event.current.type == EventType.Repaint)
		{
			FieldOfView fov = (FieldOfView)target;
			Handles.color = Color.white;

			float[] angles = new float[] { 0f, 90f, 180f, 270f };

			Vector3 center = fov.transform.position;

			foreach(float angle in angles){
				Vector3 edgeA = fov.DirFromAngle (angle);
				Handles.DrawLine (center, center + edgeA * fov.viewRadius);
			}

			Vector3 arcNormal = Vector3.Cross (fov.DirFromAngle (0f), fov.transform.up);
			Handles.DrawWireArc (center, arcNormal, fov.DirFromAngle (0f), fov.viewAngle, fov.viewRadius);

			arcNormal = Vector3.Cross (fov.DirFromAngle (90f), fov.transform.up);
			Handles.DrawWireArc (center, arcNormal, fov.DirFromAngle (90f), fov.viewAngle, fov.viewRadius);
		}
	}
}
