using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGetterForUI : MonoBehaviour {

    public AimScale AS;

    Vector3 initScale;

	// Use this for initialization
	void Start () {
        initScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = initScale * AS.aimScale;
	}
}
