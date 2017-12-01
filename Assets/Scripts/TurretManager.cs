using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour {

	public List<Transform> turrets;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleTurrets(){
        if (turrets != null)
        {
            foreach (Transform turret in turrets)
            {
                TurretControl tc = turret.GetComponentInChildren<TurretControl>();
                tc.ToggleRotate();
                tc.ToggleShoot();
            }
        }
    }
}
