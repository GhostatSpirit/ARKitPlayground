using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ObjectDeadEventArgs : EventArgs {
	public GameObject attacker { get; private set; }
	public GameObject attacked { get; private set; }
	public Collision collision { get; private set; }
	public Vector3 hitPoint { get; private set; }

	public ObjectDeadEventArgs(GameObject attacker, GameObject attacked, Collision coll){
		this.attacker = attacker;
		this.attacked = attacked;
		this.collision = coll;
		this.hitPoint = coll.contacts [0].point;
	}

	public ObjectDeadEventArgs(GameObject attacker, GameObject attacked, Vector3 hitPoint){
		this.attacker = attacker;
		this.attacked = attacked;
		this.hitPoint = hitPoint;
	}
}

public class ObjectHurtEventArgs : EventArgs {
	public GameObject attacker { get; private set; }
	public int damage { get; private set; }
	public Collision collision { get; private set; }
	public Vector3 hitPoint { get; private set; }

	public ObjectHurtEventArgs
		(GameObject attacker, int damage, Collision coll)
	{
		this.attacker = attacker;
		this.damage = damage;
		this.collision = coll;
		this.hitPoint = coll.contacts [0].point;
	}

	public ObjectHurtEventArgs
	(GameObject attacker, int damage, Vector3 hitPoint)
	{
		this.attacker = attacker;
		this.damage = damage;
		this.hitPoint = hitPoint;
	}
}

public class HealthChangedEventArgs : EventArgs {
	public int curHealth { get; private set; }
	public int maxHealth { get; private set; }

	public HealthChangedEventArgs(int curHealth, int maxHealth)
	{
		this.curHealth = curHealth;
		this.maxHealth = maxHealth;
	}
}


public class HealthSystem : MonoBehaviour {

	public int maxHealth = 100;

	public int currentHealth { get; private set; }
	public bool isDead {get; private set; }
	public bool isImmune { get; private set; }

	public event EventHandler<ObjectDeadEventArgs> OnObjectDead;
	public event EventHandler<ObjectHurtEventArgs> OnObjectHurt;
	public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

//	public void DoDamage (int damage, GameObject attacker, Collision coll);
//	public void DoDamage (int damage, GameObject attacker, Vector3 hitPoint);
//	public void StartImmune ();
//	public void EndImmune();
//	public void DieInstantly (GameObject attacker, Collision coll);
//	public void DieInstantly (GameObject attacker, Vector3 hitPoint);

	public bool destoryOnDead = false;

	public Text healthText;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		isDead = false;
		isImmune = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthText) {
			healthText.text = "Health: " + currentHealth.ToString ();
		}
	}

	void HandleHealthChange(int deltaHealth){
		if (!isImmune) {
			currentHealth += deltaHealth;
			if (currentHealth < 0)
				currentHealth = 0;

			if (OnHealthChanged != null) {
				var args = new HealthChangedEventArgs (currentHealth, maxHealth);
				OnHealthChanged (gameObject, args);
			}
		}
	}

	public void DoDamage(int damage, GameObject attacker, Collision coll){
		HandleHealthChange (-damage);

		int realDamage = isImmune ? 0 : damage;
		if (OnObjectHurt != null) {
			var args = new ObjectHurtEventArgs (attacker, realDamage, coll);
			OnObjectHurt (gameObject, args);
		}

		if(!isDead && currentHealth == 0){
			isDead = true;
			if(OnObjectDead != null){
				var eventArgs = new ObjectDeadEventArgs (attacker, gameObject, coll);
				OnObjectDead (gameObject, eventArgs);
			}
			if(destoryOnDead){
				Destroy (this.gameObject);
			}
		}
	}

	public void DoDamage(int damage, GameObject attacker, Vector3 hitPoint){
		HandleHealthChange (-damage);

		int realDamage = isImmune ? 0 : damage;
		if (OnObjectHurt != null) {
			var args = new ObjectHurtEventArgs (attacker, realDamage, hitPoint);
			OnObjectHurt (gameObject, args);
		}
			
		if(!isDead && currentHealth == 0){
			isDead = true;
			if(OnObjectDead != null){
				var eventArgs = new ObjectDeadEventArgs (attacker, gameObject, hitPoint);
				OnObjectDead (gameObject, eventArgs);
			}
			if(destoryOnDead){
				Destroy (this.gameObject);
			}
		}
	}

	public void StartImmune (){
		if(!isImmune){
			isImmune = true;
		}
	}

	public void EndImmune (){
		if(isImmune){
			isImmune = false;
		}
	}

	public void DieInstantly (GameObject attacker, Collision coll, bool ignoreImmune = false){
		if(ignoreImmune){
			EndImmune ();
		}
		DoDamage (currentHealth, attacker, coll);
	}

	public void DieInstantly (GameObject attacker, Vector3 hitPoint, bool ignoreImmune = false){
		if(ignoreImmune){
			EndImmune ();
		}
		DoDamage (currentHealth, attacker, hitPoint);
	}

}
