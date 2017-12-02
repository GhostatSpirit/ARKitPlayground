using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARScale : MonoBehaviour {

    public float unit = 0.1f;
    public Vector3 unitVector
    {
        get
        {
            return new Vector3(unit, unit, unit);
        }
    }

}
