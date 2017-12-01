using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetting : MonoBehaviour {


    private BoxDoorControl boxDoorControl;
    private int direction=4;
    private bool active=false;
    private bool handicapped = false;
    private float halfSize = 0.5f;
    private DoorDirection ddir;
    private Transform shadowCollider;

    // Use this for initialization
    void Start() {
        boxDoorControl = GetComponent<BoxDoorControl>();
        Transform[] childrenEle;
        childrenEle = transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in childrenEle)
        {
            if (i.name == "shadowCollider")
            {
                shadowCollider = i;
                break;
            }
        }
    }



    // Update is called once per frame
    void Update() {

    }



    public int getDirection() {
        return direction;
    }
    public bool isTurretActive() {
        return active&&(!handicapped);
    }
    public bool isHandicapped() {
        return handicapped;
    }

    public void beHandicapped() {
        
        disableBase();
        Debug.Log("turret handicapped");
        handicapped = true;
                
        
        
    }

    

    void disableBase()
    {
        shadowCollider.GetComponent<BoxCollider>().center = new Vector3(0,0,0);
        shadowCollider.GetComponent<BoxCollider>().size = new Vector3(1,1,1);
        boxDoorControl.StartCoroutine(boxDoorControl.TurrentAndDoor(ddir, false));
        active = false;
    }

    void enableBase(DoorDirection doorDirection, bool reverseXp, Vector3 center, Vector3 size)
    {
        shadowCollider.GetComponent<BoxCollider>().center = center;
        shadowCollider.GetComponent<BoxCollider>().size = size;
            
        ddir = doorDirection;
        boxDoorControl.StartCoroutine(boxDoorControl.TurrentAndDoor(doorDirection, reverseXp));
        active = true;
    }

    
    public void setTurretPosi(int dir) {
        direction = dir;
        switch (direction)
        {
            case 0:// +z direction
                        
                enableBase(DoorDirection.zp,true,new Vector3(0,0,halfSize),new Vector3(1,1,2));
                break;
            case 1:// +x direction
                enableBase(DoorDirection.xp, true, new Vector3(halfSize, 0, 0), new Vector3(2, 1, 1));
                break;
            case 2:// -z direction
                enableBase(DoorDirection.zn, true, new Vector3(0, 0, -halfSize), new Vector3(1, 1, 2));
                break;
            case 3:// -x direction
                enableBase(DoorDirection.xn, true, new Vector3(-halfSize, 0, 0), new Vector3(2, 1, 1));
                break;
            case 4:// +y direction
                enableBase(DoorDirection.yp, true, new Vector3(0, halfSize, 0), new Vector3(1, 2, 1));
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
