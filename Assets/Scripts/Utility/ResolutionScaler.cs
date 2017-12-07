using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionScaler : MonoBehaviour {

	void OnAwake(){
		var screenPixels = Screen.width * Screen.height;
		if(screenPixels > 2074000){
			Screen.SetResolution (Screen.width / 2, Screen.height / 2, true, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
