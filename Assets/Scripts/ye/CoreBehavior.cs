using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CoreBehavior : MonoBehaviour {

    HealthSystem HS;

	public List<Collider> playerColliders = new List<Collider>();
	public GameObject congratMenu;


	// Use this for initialization
	void Start () {
        HS = GetComponent<HealthSystem>();
        HS.OnObjectDead += CoreDead;
	}

	void CoreDead(object hurtObj, EventArgs args)
	{
//		StartCoroutine (congShow ());
	}

}
