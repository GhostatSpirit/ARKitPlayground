using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonDisabler : MonoBehaviour {

    public CubeBuilder cubeBuiler;

    Button button;
    // Use this for initialization
    void Start () {
        button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		if(cubeBuiler.fallingCount > 0)
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
	}
}
