using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeBoxMotion : MonoBehaviour
{

    public Transform arrangeCamera;
    public Transform arrangeCube;


    int groundMask;
    public float range = 100f;
    // Use this for initialization
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Ray shootRay = new Ray();
        RaycastHit shootHit;
        shootRay.origin = arrangeCamera.position;
        shootRay.direction = arrangeCamera.forward;
        if (Physics.Raycast(shootRay, out shootHit, range, groundMask))
        {
            Vector3 hitPoint = shootHit.point;

            arrangeCube.position = new Vector3((float)((int)hitPoint.x), (float)((int)hitPoint.y), (float)((int)hitPoint.z));

        }
    }
}
