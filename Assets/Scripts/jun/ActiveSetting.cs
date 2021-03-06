﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveSetting : MonoBehaviour {

 
    private BoxDoorControl boxDoorControl;
    private InnerFuncControl innerFuncControl;
    private int direction=4;
    private bool active=false;
   // private bool handicapped = false;
    private float halfSize ;
    private DoorDirection ddir;
    private Transform shadowCollider;
    private bool disordered=false;
    

    // Use this for initialization
    void Start() {

        boxDoorControl = GetComponent<BoxDoorControl>();
        innerFuncControl = GetComponent<InnerFuncControl>();
        if (boxDoorControl == null && innerFuncControl==null)
        {
            disordered = true;
        }
        //get shadowCollider
        
        foreach (Transform i in transform.GetComponentsInChildren<Transform>())
        {
            if (i.name == "shadowCollider")
            {
                shadowCollider = i;
                break;
            }
        }
        halfSize = shadowCollider.GetComponent<BoxCollider>().size.x / 2;
    }






    // Update is called once per frame
    void Update() {

    }

    public bool neededToActivate() {
        if (active == false && disordered == false) {
            return true;
        }
        return false;
    }

    public int getDirection() {
        return direction;
    }

    private bool hasFunction() {
        if (boxDoorControl != null || innerFuncControl !=null) { return true; }
        return false;
    }

    public void removeTurret() {
        shadowCollider.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0);
        shadowCollider.GetComponent<BoxCollider>().size = new Vector3(2 * halfSize, 2 * halfSize, 2 * halfSize);
        disordered = true;
    }

    public void disableBase()
    {
        if (disordered == true) { return; }
        if (!hasFunction()) { return; }
        shadowCollider.GetComponent<BoxCollider>().center = new Vector3(0,0,0);
        shadowCollider.GetComponent<BoxCollider>().size = new Vector3(2 * halfSize, 2 * halfSize, 2 * halfSize);
        boxDoorControl.StartCoroutine(boxDoorControl.TurrentAndDoor(ddir, false));
        active = false;
    }

    void enableBase(DoorDirection doorDirection, bool reverseXp, Vector3 center, Vector3 size)
    {
        if (!hasFunction()) { return; }
        
        
        if (boxDoorControl != null)
        {
            shadowCollider.GetComponent<BoxCollider>().center = center;
            shadowCollider.GetComponent<BoxCollider>().size = new Vector3(2 * halfSize, 2 * halfSize, 2 * halfSize);
            shadowCollider.GetComponent<BoxCollider>().size += 2 * halfSize * size;
            ddir = doorDirection;
            boxDoorControl.StartCoroutine(boxDoorControl.TurrentAndDoor(doorDirection, reverseXp));

        }
        if (innerFuncControl != null) {
            innerFuncControl.BarrierActivate();
        }

        active = true;
    }

    
    public void setTurretPosi(int dir) {
        if (disordered == true) { return; }
        if (!hasFunction()) { return; }
        direction = dir;
        switch (direction)
        {
            case 0:// +z direction
                        
                enableBase(DoorDirection.zp,true,new Vector3(0,0,halfSize),new Vector3(0,0,1));
                break;
            case 1:// +x direction
                enableBase(DoorDirection.xp, true, new Vector3(halfSize, 0, 0), new Vector3(1,0,0));
                break;
            case 2:// -z direction
                enableBase(DoorDirection.zn, true, new Vector3(0, 0, -halfSize), new Vector3(0,0,1));
                break;
            case 3:// -x direction
                enableBase(DoorDirection.xn, true, new Vector3(-halfSize, 0, 0), new Vector3(1,0,0));
                break;
            case 4:// +y direction
                enableBase(DoorDirection.yp, true, new Vector3(0, halfSize, 0), new Vector3(0,1,0));
                break;
            case 5:
                // disableBase(i);
                break;
            default:
                // disableBase(i);
                break;
        }
        
    }

    public void setTurretPosi(Vector3 directionV)
    {
        if (disordered == true) { return; }
        if (!hasFunction()) { return; }
        direction = -1;
        if (directionV.y == 1) { direction = 4; }
        else if (directionV.x == 1) { direction = 1; }
        else if (directionV.x == -1) { direction = 3; }
        else if (directionV.z == -1) { direction = 2; }
        else if (directionV.z == 1) { direction = 0; }
        else { direction = 5; }
        
        setTurretPosi(direction);
    }
}
