using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationButton : MonoBehaviour {

    public GameObject[] buttons;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activeButtons()
    {
        for (int i = 0; i< buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }
}
