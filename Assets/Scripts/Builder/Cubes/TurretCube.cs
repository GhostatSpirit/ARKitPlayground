using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretCube", menuName = "CubeData/TurretCube")]
public class TurretCube : BaseCube {

    public GameObject turretAnchor;
    public GameObject turret;

    public int turretHealth = 50;
	
}
