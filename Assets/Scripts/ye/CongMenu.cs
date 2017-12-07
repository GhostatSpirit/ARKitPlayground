using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongMenu : MonoBehaviour {

    public GameObject[] offgameObjects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject gameObject in offgameObjects)
        {
            gameObject.SetActive(false);
        }
	}
}
