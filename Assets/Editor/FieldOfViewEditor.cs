using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor {

	float angleStep = 60f;

	protected virtual void OnSceneGUI()
	{
		if (Event.current.type == EventType.Repaint)
		{
			FieldOfView fov = (FieldOfView)target;
			Handles.color = Color.white;


			Vector3 center = fov.pivot.position;

			for(float angle = 0f; angle < 180f; angle += angleStep){
				// draw the two facing edge rays
				float length = fov.viewRadius;
				if(length == Mathf.Infinity){
					length = 1f;
				}

				Vector3 edgeA = fov.DirFromAngle (angle);
				Handles.DrawLine (center, center + edgeA * length);
				Vector3 edgeB = fov.DirFromAngle (angle + 180f);
				Handles.DrawLine (center, center + edgeB * length);

				// draw the curve that connects the two rays
				Vector3 arcNormal = Vector3.Cross (fov.DirFromAngle (angle), fov.pivot.up);
				Handles.DrawWireArc (center, arcNormal, fov.DirFromAngle (angle), fov.viewAngle, length);
			}

			Handles.color = Color.red;
			foreach (Transform visibleTarget in fov.visibleTargets) {
				Handles.DrawLine (center, visibleTarget.position);
			}
				
		}
	}
}
