﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }	
	}
}
