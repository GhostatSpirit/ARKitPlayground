using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour {

	public int maxHealth = 100;

	public int currentHealth;
	bool isDead = false;

	public event Action<Transform> OnObjectDead;

	public bool destoryOnDead = false;

	public Text healthText;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthText) {
			healthText.text = "Health: " + currentHealth.ToString ();
		}
	}

	public void DoDamage(int damage){
		currentHealth -= damage;
		if (currentHealth < 0)
			currentHealth = 0;

		if(!isDead && currentHealth == 0){
			isDead = true;
			if(OnObjectDead != null){
				OnObjectDead (transform);
			}
			if(destoryOnDead){
				Destroy (this.gameObject);
			}
		}
	}
		
}
