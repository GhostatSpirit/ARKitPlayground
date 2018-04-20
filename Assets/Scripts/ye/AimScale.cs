using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimScale : MonoBehaviour {

    [HideInInspector]public float aimScale = 1;

    Quaternion postRotation;

    Vector3 postPosition;

    Transform parentTrans;

    public ShootingParam sp;

    public float aimTime;

    public bool scaleOnMoving;

    public float movingScaleLimit;

    public float overheatScaleLimit;

    public float overheatScale;

    float movingScale;

    public float movingScaleUpk = 1;

    public float movingScaleDownk = 1;

    public float overheatScaleUp = 0.5f;

    public float overheatScaleDown = 0.5f;

    [HideInInspector]public float movingScaleActiveAngle = 0;

    [HideInInspector]public float movingScaleActiveDistance = 0;

    float timeCount = 0;

    [HideInInspector]public float rotationAngle = 0;

    [HideInInspector]public float movingDistance = 0;

	void Start () {
        movingScale = 1;
        overheatScale = 0;
        parentTrans = transform.parent;
        postRotation = parentTrans.rotation;
        postPosition = parentTrans.position;

        //Debug.Log(aimTime);
	}
	
	void Update () {
        movingScaleActiveDistance = sp.movingScaleActiveDistance;
        movingScaleActiveAngle = sp.movingScaleActiveAngle;
        aimTime = sp.aimTime;
        movingScaleLimit = sp.movingScaleLimit;
        overheatScaleLimit = sp.overheatScaleLimit;
        movingScaleUpk = sp.movingScaleUpk;
        movingScaleDownk = sp.movingScaleDownk;
        overheatScaleUp = sp.overheatScaleUp;
        overheatScaleDown = sp.overheatScaleDown;

        //sp.aimTime = aimTime;
        //sp.scaleLimit = scaleLimit;
        //sp.scaleUpk = scaleUpk;
        //sp.scaleDownk = scaleDownk;

        if (scaleOnMoving)
        {
            rotationAngle = Quaternion.Angle(parentTrans.rotation, postRotation);
            movingDistance = Vector3.Distance(postPosition, parentTrans.position);
            if (parentTrans.rotation != postRotation || parentTrans.position != postPosition)
            {
                if(rotationAngle > movingScaleActiveAngle || movingDistance > movingScaleActiveDistance)
                {
                    movingScale += Time.deltaTime * movingScaleUpk;
                    timeCount = 0;
                }
                else
                {
                    timeCount += Time.deltaTime;
                    if (timeCount >= aimTime)
                    {
                        timeCount = aimTime;
                    }
                }

                //if (movingDistance >= movingScaleActiveDistance)
                //{
                //    movingScale += Time.deltaTime * movingScaleUpk;
                //}

                if (movingScale > movingScaleLimit)
                {
                    movingScale = movingScaleLimit;
                }

                
            }
            else
            {
                timeCount += Time.deltaTime;
                if (timeCount >= aimTime)
                {
                    timeCount = aimTime;
                }
            }

            if (timeCount == aimTime)
            {
                if (movingScale - Time.deltaTime * movingScaleDownk>= 1)
                {
                    movingScale -= Time.deltaTime * movingScaleDownk;
                }
                if (movingScale > 1 && movingScale - Time.deltaTime * movingScaleDownk < 1)
                {
                    movingScale = 1;
                }
            }
            postPosition = parentTrans.position;
            postRotation = parentTrans.rotation;
        }
        else
        {
            movingScale = 1;
        }

        aimScale = overheatScale + movingScale;
	}

    public void activeDistanceAdd()
    {
        sp.movingScaleActiveDistance += 0.003f;
    }

    public void activeDistanceMinus()
    {
        sp.movingScaleActiveDistance -= 0.003f;
    }

    public void activeAngleAdd()
    {
        sp.movingScaleActiveAngle += 0.1f;
    }

    public void activeAngleMinus()
    {
        sp.movingScaleActiveAngle -= 0.1f;
    }
}
