using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {
	// Use this for initialization
	void Start () {
		if(Time.timeScale == 0)
		{
			Debug.Log ("reset timeScale in StartScene");
			Time.timeScale = 1;
		}	
	}

	void OnEnable(){
		if(Time.timeScale == 0)
		{
			Debug.Log ("reset timeScale in StartScene");
			Time.timeScale = 1;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
