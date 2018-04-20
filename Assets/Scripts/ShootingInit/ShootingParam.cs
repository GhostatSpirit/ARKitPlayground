using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShootingParam", menuName = "ShootingData/ShootingParam")]
public class ShootingParam : ScriptableObject
{
    //public string displayName = "Shooting Para";
    public float aimTime;
    public float movingScaleLimit;
    public float movingScaleActiveAngle;
    public float movingScaleActiveDistance;
    public float overheatScaleLimit;
    public float movingScaleUpk;
    public float movingScaleDownk;
    public float overheatScaleUp;
    public float overheatScaleDown;
}
