using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableZWrite : MonoBehaviour {
	void Start () {
		var myMaterial = GetComponent<Renderer>().material;
		myMaterial.SetInt("_ZWrite", 0);
	}
}
