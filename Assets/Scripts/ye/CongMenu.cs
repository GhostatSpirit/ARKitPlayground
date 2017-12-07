using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongMenu : MonoBehaviour {

	public HealthSystem coreHealthSystem;
	public GameObject congratMenu;

	public float finalDelay = 1f;
	[Range(0f, 1f)]
	public float finalTimeScale = 0f;

	public Collider[] playerColliders;
	public GameObject[] offgameObjects;

	bool startedCongrat = false;

	// Use this for initialization
	void Start () {
		if(coreHealthSystem != null){
			coreHealthSystem.OnObjectDead += StartCongrat;
		}
	}

	void StartCongrat(object sender, ObjectDeadEventArgs args){
		Debug.Log ("started!");
		congratMenu.SetActive (true);
		if (!startedCongrat) {
			StartCoroutine (congShow());
			startedCongrat = true;
		}
	}


	IEnumerator congShow()
	{

		yield return new WaitForSeconds (finalDelay);

		Time.timeScale = finalTimeScale;

		foreach(Collider coll in playerColliders){
			if (coll) {
				coll.enabled = false;
			}
		}


		foreach(GameObject gameObject in offgameObjects)
		{
			gameObject.SetActive(false);
		}

	}
}
