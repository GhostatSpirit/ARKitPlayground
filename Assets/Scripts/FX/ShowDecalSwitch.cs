using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDecalSwitch : MonoBehaviour {

	public GameObject decal;

	public void TurnOn(){
		if(decal){
			decal.SetActive (true);
		}
	}

	public void TurnOff(){
		if(decal){
			decal.SetActive (false);
		}
	}
}
