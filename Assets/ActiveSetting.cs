using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetting : MonoBehaviour {
    public Vector3 disablePosi;
    public Vector3 enablePosi;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isTurretActive() {
        Transform[] childrenEle;
        childrenEle = transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in childrenEle)
        {
            if (i.name == "Base")
            {
                if (i.localPosition == disablePosi) { return false; }
                //if (i.gameObject.activeSelf == false) { return false; }
                else { return true; }
            }
        }
        return false;
    }

    public void turretDisablePosi() {
        Transform[] childrenEle;
        childrenEle= transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in childrenEle) {
            if (i.name == "Base") {
                //i.gameObject.SetActive(false);
                i.localPosition = disablePosi;
            }
        }
    }
   
    public void setTurretPosi(Vector3 directionV)
    {
        int direction = -1;
        if (directionV.y == 1) { direction = 4; }
        else if (directionV.x == 1) { direction = 1; }
        else if (directionV.x == -1) { direction = 3; }
        else if (directionV.z == -1) { direction = 2; }
        else if (directionV.z == 1) { direction = 0; }
        else { direction = 5; }

        Debug.Log("direction:" + direction);

        Transform[] childrenEle;

        childrenEle = transform.GetComponentsInChildren<Transform>();
        foreach (Transform i in childrenEle)
        {
            if (i.name == "Base")
            {

                Debug.Log("direction again:" + direction);
                switch (direction)
                {

                    case 0:// +z direction
                        i.localPosition = new Vector3(0, 0, enablePosi.y);
                        i.localEulerAngles = new Vector3(0, 90f, 90f);
                        //i.gameObject.SetActive(true);
                        break;
                    case 1:// +x direction
                        i.localPosition = new Vector3(enablePosi.y, 0, 0);
                        i.localEulerAngles = new Vector3(0, 0f, -90f);
                        //i.gameObject.SetActive(true);
                        break;
                    case 2:// -z direction
                        i.localPosition = new Vector3(0, 0, -enablePosi.y);
                        i.localEulerAngles = new Vector3(0, 90f, -90f);
                        //i.gameObject.SetActive(true);
                        break;
                    case 3:// -x direction
                        i.localPosition = new Vector3(-enablePosi.y, 0, 0);
                        i.localEulerAngles = new Vector3(0, 0f, 90f);
                        //i.gameObject.SetActive(true);
                        break;
                    case 4:// +y direction
                        i.localPosition = enablePosi;
                        i.localEulerAngles = new Vector3(0, 0f, 0f);
                        // i.gameObject.SetActive(true);
                        break;
                    case 5:
                        i.localPosition = disablePosi;
                        //i.gameObject.SetActive(false);
                        //transform.GetComponent<TurretControlT>
                        break;
                    default:
                        i.localPosition = disablePosi;
                        break;
                }

            }
        }
    }
}
