using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlay_BulletHoleTexture : MonoBehaviour {
	
	[HideInInspector]public Texture2D texture0 { get; private set;}
	[HideInInspector]public Texture2D texture1 { get; private set;}

	// Use this for initialization
	void Start () {
		texture0 = Resources.Load("CameraPlay_BulletHole_Anm") as Texture2D;
		texture1 = Resources.Load("CameraPlay_BulletHole_Anm2") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
