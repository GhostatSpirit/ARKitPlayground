using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour {
	public int m_damage = 10;

	public abstract void FireProjectile(GameObject launcher, GameObject target, Vector3 direction, int damage);

}
