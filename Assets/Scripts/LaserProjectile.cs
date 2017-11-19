using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : BaseProjectile {

	public float m_duration = 1f;
	public float m_maxDistance = 1f;

	public LayerMask m_targetMask;

	Vector3 m_direction;
	bool m_fired;
	GameObject m_launcher;

	LineRenderer m_lr;

	float m_timer = 0f;

	// Use this for initialization
	void Start () {
		m_lr = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_fired){
			m_lr.enabled = true;
			Ray ray = new Ray (transform.position, m_direction);
			RaycastHit hit;
			Vector3 endPosition;

			if(Physics.Raycast(ray, out hit, m_maxDistance, m_targetMask)){
				endPosition = hit.point;
				//TODO: instantiate particle system for the hit
			} else {
				endPosition = transform.position + m_direction * m_maxDistance;
			}

			Vector3[] linePoints = new Vector3[2];
			linePoints [0] = transform.position;
			linePoints [1] = endPosition;
			m_lr.SetPositions (linePoints);

			m_timer += Time.deltaTime;
			if(m_timer >= m_duration){
				m_fired = false;
				m_timer = 0f;
			}
		}
	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		m_damage = damage;
		if(launcher){
			m_launcher = launcher;
			if (direction != Vector3.zero) {
				m_direction = direction.normalized;
			} else {
				m_direction = (target.transform.position - launcher.transform.position).normalized;
			}
			m_fired = true;
		}
	}

}
