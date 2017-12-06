using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour {

    public PlayerShootingNew PSN1;
    public PlayerShootingNew PSN2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            PSN1.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            PSN1.enabled = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            PSN2.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            PSN2.enabled = false;
        }

    }
}
