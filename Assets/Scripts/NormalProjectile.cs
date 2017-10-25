using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : BaseProjectile {

	Vector3 m_direction;
	bool m_fired;
	
	// Update is called once per frame
	void Update () {
		if(m_fired){
			transform.position += m_direction * speed * Time.deltaTime;
		}
	}

	public override void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage){
		if(launcher && target){
			if (direction != Vector3.zero) {
				m_direction = direction;
			} else {
				m_direction = (target.transform.position - launcher.transform.position).normalized;
			}
			m_fired = true;
		}
	}
}
