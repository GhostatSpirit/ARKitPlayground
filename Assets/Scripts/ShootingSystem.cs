﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {
	public float fireRate;
	public int damage;

	public float fieldOfView;


	// public bool beam;
	public GameObject projectile;
	public GameObject target;
	public List<GameObject> projectileSpawns;


	List<GameObject> m_lastProjectiles = new List<GameObject> ();
	float m_fireTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_fireTimer += Time.deltaTime;

		if(m_fireTimer >= fireRate){
			float angle = Quaternion.Angle
				(transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position));

			Debug.Log ("angle is " + angle.ToString ());

			if(angle < fieldOfView){
				SpawnProjectiles ();
				m_fireTimer = 0f;
			}
		}
	}

	void SpawnProjectiles(){
		if(!projectile){
			return;
		}

		for(int i = 0; i < projectileSpawns.Count; i++){
			if(projectileSpawns[i]){
				GameObject proj = Instantiate (projectile, projectileSpawns [i].transform.position,
					                  Quaternion.Euler (projectileSpawns [i].transform.forward)) as GameObject;
				proj.GetComponent<BaseProjectile> ().FireProjectile (projectileSpawns [i], target, damage);

				m_lastProjectiles.Add (proj);
			}
		}
	}
}
