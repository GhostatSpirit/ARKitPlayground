using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {
	public float fireRate = 1f;
	public int damage = 5;

	public float fieldOfView = 10f;

	public Transform pitchSegment;
	// public bool beam;
	public GameObject projectile;
	public Transform target;
	public List<GameObject> projectileSpawns;


	List<GameObject> m_lastProjectiles = new List<GameObject> ();
	float m_fireTimer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_fireTimer += Time.deltaTime;

		if (target == null) {
			return;
		}

		if(m_fireTimer >= fireRate){
//			float angle = Quaternion.Angle
//				(transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position));
			Quaternion tempQuaternion = Quaternion.FromToRotation (pitchSegment.forward, 
				target.transform.position - pitchSegment.position);
			float angle = Quaternion.Angle (tempQuaternion, Quaternion.identity);

			// Debug.Log ("angle is " + angle.ToString ());

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

				Vector3 oldScale = proj.transform.localScale;

				Vector3 localScale = transform.localScale;
				Vector3 lossyScale = transform.lossyScale;

				Vector3 scaleFactor = new Vector3(lossyScale.x / localScale.x,
					lossyScale.y / localScale.y, lossyScale.z / localScale.z);


				Vector3 newScale = new Vector3(oldScale.x * scaleFactor.x,
					oldScale.y * scaleFactor.y, oldScale.z * scaleFactor.z);
				proj.transform.localScale = newScale;

				proj.transform.forward = projectileSpawns [i].transform.forward;

				proj.GetComponent<BaseProjectile> ()
					.FireProjectile (projectileSpawns [i], target.gameObject,
						             projectileSpawns[i].transform.forward, damage);

				m_lastProjectiles.Add (proj);
			}
		}
	}
}
