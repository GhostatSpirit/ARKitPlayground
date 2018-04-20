using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryWhenDead : MonoBehaviour {

	bool isSet = false;
	HealthSystem targetHS;

	void DestroySelf(object sender, ObjectDeadEventArgs args){
		// Debug.Log ("called!");
		Destroy (this.gameObject);
	}

	public void SetHealthSystem(HealthSystem hs){
		if(hs && !isSet){
			targetHS = hs;
			hs.OnObjectDead += DestroySelf;
			isSet = true;
		}
	}

	void OnDestroy(){
		if (targetHS && targetHS.currentHealth != 0) {
			targetHS.OnObjectDead -= DestroySelf;
		}
	}
}
