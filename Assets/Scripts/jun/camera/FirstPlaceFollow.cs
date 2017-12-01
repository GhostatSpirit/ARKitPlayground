using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlaceFollow : MonoBehaviour {

    public float sensitivityX = 10F;
    public float sensitivityY = 10F;
    Vector3 offset;
    public float minimumY = -60f;
    public float maximumY = 60f;
    public float smoothing = 1f;
    public Transform target;
    Rigidbody playerRigidbody;
    //private Vector3 rotation;
    float rotationX;
    float rotationY = 0f;
    // Use this for initialization
    void Update () {
        rotationX = target.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        //Quaternion q = Quaternion.AngleAxis(target.localEulerAngles.y, new Vector3(0, 1, 0));
        //Vector3 targetCamPos = target.position + q * offset;

        //transform.position = targetCamPos;


        //Vector3 rotation = new Vector3(-rotationY, rotationX, 0);
      //  transform.localEulerAngles = rotation;
       // rotation.x = 0;
        Quaternion objq = Quaternion.AngleAxis(rotationX, new Vector3(0, 1, 0));
        //target.localEulerAngles = rotation;
        playerRigidbody.MoveRotation(objq);
    }

    private void LateUpdate()
    {
        Quaternion q = Quaternion.AngleAxis(target.localEulerAngles.y, new Vector3(0, 1, 0));
        Vector3 targetCamPos = target.position + q * offset;

        transform.position = targetCamPos;
        transform.localEulerAngles = new Vector3(-rotationY,target.localEulerAngles.y, 0);
       // transform.LookAt(target);
    }

    // Update is called once per frame
    void Start () {
        //target = transform.parent;
        offset = transform.position - target.position;
        playerRigidbody = target.GetComponent<Rigidbody>();
    }
}
