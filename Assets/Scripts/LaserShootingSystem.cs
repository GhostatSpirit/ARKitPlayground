using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShootingSystem : MonoBehaviour {

	bool needAim = false;
	public float aimDuration = 3f;
	public float shootDuration = 1f;

	public int damage = 5;
	public float fieldOfView = 10f;

	public Transform pitchSegment;
	public Transform target;
	public List<GameObject> projectileSpawns;
// 	RotateTowards rt;


	// Use this for initialization
	void Start () {
//		rt = GetComponent<RotateTowards> ();
		StartCoroutine (LaserLogicIE ());
	}



	IEnumerator LaserLogicIE(){
		while(true){
			if (needAim) {
				yield return new WaitForSeconds (aimDuration);
			}
			yield return new WaitUntil (() => isInSight ());

			// rt.enabled = false;
			SpawnProjectiles ();

			yield return new WaitForSeconds (shootDuration);

			// rt.enabled = true;
			DisableProjectiles ();
		}
	}

	void SpawnProjectiles(){
		if(projectileSpawns == null){
			return;
		}

		for (int i = 0; i < projectileSpawns.Count; i++) {
			GameObject spawn = projectileSpawns[i];
			if (spawn) {
				BaseProjectile bp = spawn.GetComponent<BaseProjectile> ();
				if(bp){
					GameObject targetGO = (target != null) ? target.gameObject : null;
					bp.FireProjectile (spawn, targetGO, spawn.transform.forward, damage);
				}
			}
		}
	}

	void DisableProjectiles(){
		for (int i = 0; i < projectileSpawns.Count; i++) {
			GameObject spawn = projectileSpawns[i];
			if (spawn) {
				BaseProjectile bp = spawn.GetComponent<BaseProjectile> ();
				if(bp){
					bp.enabled = false;
				}
			}
		}
	}

	bool isInSight(){
		if(target == null){
			return false;
		}

		Quaternion tempQuaternion = Quaternion.FromToRotation (pitchSegment.forward, 
			target.transform.position - pitchSegment.position);
		float angle = Quaternion.Angle (tempQuaternion, Quaternion.identity);

		// Debug.Log ("angle is " + angle.ToString ());

		return angle <= fieldOfView / 2.0f;
	}
}
