using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UltimateFracturing;

public class BaseBlock : MonoBehaviour {

	public float deadExplodeForce = 1000f;

	HealthSystem hs;
	FracturedObject fo;

	// Use this for initialization
	void Start () {
		hs = GetComponent<HealthSystem> ();
		fo = GetComponent<FracturedObject> ();
		if(hs){
			hs.OnObjectDead += Explode;
		}
	}

	void Explode(object obj, EventArgs args){
		ObjectDeadEventArgs dargs = (ObjectDeadEventArgs)args;

		Vector3 impactPos;
		if(dargs.collision != null){
			Vector3 hitPos = dargs.collision.contacts [0].point;
			Vector3 hitToCenter = fo.transform.position - hitPos;

			impactPos = hitPos + hitToCenter * 2f;

			// impactPos = fo.transform.position;
		} else {
			impactPos = fo.transform.position;
		}

		fo.Explode (impactPos, deadExplodeForce);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
