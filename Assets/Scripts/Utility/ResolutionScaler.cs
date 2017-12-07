﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScaler : MonoBehaviour {

	void Start(){
		var screenPixels = Screen.width * Screen.height;
		Debug.Log ("screenPixels: " + screenPixels.ToString ());
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
		if(screenPixels > 2074000){
			Debug.Log ("setting resolution");
			Screen.SetResolution (Screen.width / 2, Screen.height / 2, true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
