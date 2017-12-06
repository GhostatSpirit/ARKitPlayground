using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathExplosion : MonoBehaviour {

	public GameObject explosionPrefab;

	HealthSystem hs;

	// Use this for initialization
	void Start () {
		hs = GetComponent<HealthSystem> ();
		if(hs){
			hs.OnObjectDead += SpawnExplosion;
		}
	}

	void SpawnExplosion(object sender, EventArgs args){
		GameObject deadGO = (GameObject)sender;
		Transform deadTrans = deadGO.transform;
		Instantiate (explosionPrefab, deadTrans.position, deadTrans.rotation);
	}

}
