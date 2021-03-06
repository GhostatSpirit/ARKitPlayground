﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection { xp, xn, yp, yn, zp, zn };

public class BoxDoorControl : MonoBehaviour
{

    DoorDirection doorDirection;
    [HideInInspector]
    public GameObject doorXp, doorXn, doorYp, doorYn, doorZp, doorZn;

    public GameObject turrent;

    Animation doorXpAnim;
    Animation doorXnAnim;
    Animation doorYpAnim;
    Animation doorYnAnim;
    Animation doorZpAnim;
    Animation doorZnAnim;

    Animation turrentAnim;

    bool reverseXp = true;
    bool reverseXn = true;
    bool reverseYp = true;
    bool reverseYn = true;
    bool reverseZp = true;
    bool reverseZn = true;

    GameObject instantiateTurrent;

    private GameObject cube;
    private DoorDirection ddir = DoorDirection.yn;


    Vector3 startPosition;
    Vector3 endPosition;


    //public Transform targetPlayer;

    public float distance = 0.5f;

    float distanceTime = 1.90f;

    float distanceTimeOut = 1.20f;

    public float TurretWaitDoor = 1.9f;

    public float DoorWaitTurret = 0f;

    bool done = false;

    [HideInInspector]
    public bool destroyed = false;

    [HideInInspector]
    public HealthSystem TurrentHS;
    [HideInInspector]
    public RotateTowards TurrentRT;
    [HideInInspector]
    public ShootingSystem TurrentSS;
    [HideInInspector]
    public LaserShootingSystem TurrentLSS;

    private HealthSystem ParentHS;

    bool set = false;

    // Use this for initialization
    void Start()
    {
        done = false;

        distanceTimeOut = 1.15f;

        doorXpAnim = doorXp.GetComponent<Animation>();
        doorXnAnim = doorXn.GetComponent<Animation>();
        doorYpAnim = doorYp.GetComponent<Animation>();
        doorYnAnim = doorYn.GetComponent<Animation>();
        doorZpAnim = doorZp.GetComponent<Animation>();
        doorZnAnim = doorZn.GetComponent<Animation>();



        foreach (Transform i in transform)
        {
            if (i.name == "cube")
            {
                cube = i.gameObject;
                break;
            }
        }

        ParentHS = cube.GetComponent<HealthSystem>();

    }

    // Update is called once per frame
    void Update()
    {


        if (TurrentHS != null && set == false)
        {
            TurrentHS.OnObjectDead += turrentDestroyed;
            set = true;
//            Debug.Log("set");
        }



    }

    public void turrentDestroyed(object source, ObjectDeadEventArgs args)
    {
        turrentDestroyed();
    }

    public void turrentDestroyed()
    {
        destroyed = true;
        ParentHS.DoDamage(TurrentHS.maxHealth, this.gameObject, this.transform.position);
        Debug.Log("parent hurt: " + ParentHS.transform.name);
        StartCoroutine(TurrentAndDoor(ddir, false));
        GetComponent<ActiveSetting>().removeTurret();
        
        Debug.Log("Destroyed!!!");
    }


    public IEnumerator TurrentAndDoor(DoorDirection doorDirection, bool reverse)
    {
        switch (reverse)
        {
            case true:
                if (destroyed == false)
                {
                    //Debug.Log("get");
                    doorAnimSingle(doorDirection, reverse);

                    yield return new WaitForSeconds(TurretWaitDoor);

                    turrentSingle(doorDirection, reverse);
                }
                else if (destroyed == true)
                {
                    doorAnimSingle(doorDirection, reverse);
                }
                break;
            case false:
                if (destroyed == false)
                {
                    turrentSingle(doorDirection, reverse);

                    yield return new WaitForSeconds(DoorWaitTurret);

                    doorAnimSingle(doorDirection, reverse);
                }
                else if (destroyed == true)
                {
                    doorAnimSingle(doorDirection, reverse);
                }
                break;
            default:
                break;
        }


    }

    public IEnumerator MoveFunction(GameObject turrent, Vector3 newPosition)
    {
        float timeSinceStarted = 0f;
        while (true)
        {
            timeSinceStarted += Time.deltaTime;
            turrent.transform.position = Vector3.Lerp(turrent.transform.position, newPosition, timeSinceStarted);

            // If the object has arrived, stop the coroutine
            if (turrent.transform.position == newPosition)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    public void turrentSingle(DoorDirection doorDirection, bool reverse)
    {
        if (destroyed == false)
        {
            ddir = doorDirection;
            switch (doorDirection)
            {
                case DoorDirection.xp:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorXp.transform.position, doorXp.transform.rotation, cube.transform);
                        
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(distanceTimeOut * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x + distanceTimeOut * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        
                        EnableComponents(reverse);
                        
                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorXp.transform.position;
                        instantiateTurrent.transform.rotation = doorXp.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(distanceTimeOut * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        // endPosition = new Vector3(startPosition.x + distanceTimeOut * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(- distanceTime * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x - distanceTime * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }

                    break;
                case DoorDirection.xn:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {

                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorXn.transform.position, doorXn.transform.rotation, cube.transform);

                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(-distanceTimeOut * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        // endPosition = new Vector3(startPosition.x - distanceTimeOut * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorXn.transform.position;
                        instantiateTurrent.transform.rotation = doorXn.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(-distanceTimeOut * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x - distanceTimeOut * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(distanceTime * distance * transform.localScale.x, 0, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        // endPosition = new Vector3(startPosition.x + distanceTime * distance * transform.localScale.x, startPosition.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }
                    break;
                case DoorDirection.yp:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorYp.transform.position, doorYp.transform.rotation, cube.transform);

                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, distanceTimeOut * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y + distanceTimeOut * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorYp.transform.position;
                        instantiateTurrent.transform.rotation = doorYp.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, distanceTimeOut * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y + distanceTimeOut * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, - distanceTime * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        // endPosition = new Vector3(startPosition.x, startPosition.y - distanceTime * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }
                    break;
                case DoorDirection.yn:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorYn.transform.position, doorYn.transform.rotation, cube.transform);

                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, -distanceTimeOut * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y - distanceTimeOut * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);

                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorYn.transform.position;
                        instantiateTurrent.transform.rotation = doorYn.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, -distanceTimeOut * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y - distanceTimeOut * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, distanceTime * distance * transform.localScale.y, 0);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y + distanceTime * distance * transform.localScale.y, startPosition.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }
                    break;
                case DoorDirection.zp:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorZp.transform.position, doorZp.transform.rotation, cube.transform);

                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, +distanceTimeOut * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceTimeOut * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorZp.transform.position;
                        instantiateTurrent.transform.rotation = doorZp.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, +distanceTimeOut * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceTimeOut * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, -distanceTime * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceTime * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }
                    break;
                case DoorDirection.zn:
                    if (instantiateTurrent == null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent = Instantiate(turrent, doorZn.transform.position, doorZn.transform.rotation, cube.transform);

                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        GetTurrentComponent(instantiateTurrent);

                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, - distanceTimeOut * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceTimeOut * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse == true && done == false)
                    {
                        done = true;

                        instantiateTurrent.transform.position = doorZn.transform.position;
                        instantiateTurrent.transform.rotation = doorZn.transform.rotation;
                        instantiateTurrent.transform.Rotate(0, 0, 180);
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, -distanceTimeOut * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceTimeOut * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                    }
                    else if (instantiateTurrent != null && reverse != true)
                    {
                        startPosition = instantiateTurrent.transform.position;

                        Vector3 transVec = new Vector3(0, 0, distanceTime * distance * transform.localScale.z);

                        transVec = transform.TransformDirection(transVec);

                        endPosition = startPosition + transVec;

                        //endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceTime * distance * transform.localScale.z);

                        StartCoroutine(MoveFunction(instantiateTurrent, endPosition));
                        EnableComponents(reverse);
                        //Destroy(instantiateTurrent);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void doorAnimSingle(DoorDirection doorDirection, bool reverse)
    {
        switch (doorDirection)
        {
            case DoorDirection.xp:
                animPlay(doorXpAnim, reverse);
                break;
            case DoorDirection.xn:
                animPlay(doorXnAnim, reverse);
                break;
            case DoorDirection.yp:
                animPlay(doorYpAnim, reverse);
                break;
            case DoorDirection.yn:
                animPlay(doorYnAnim, reverse);
                break;
            case DoorDirection.zp:
                animPlay(doorZpAnim, reverse);
                break;
            case DoorDirection.zn:
                animPlay(doorZnAnim, reverse);
                break;
            default:
                break;
        }
    }

    void doorAnimAll(bool reverse)
    {
        animPlay(doorXpAnim, reverse);
        animPlay(doorXnAnim, reverse);
        animPlay(doorYpAnim, reverse);
        animPlay(doorYnAnim, reverse);
        animPlay(doorZpAnim, reverse);
        animPlay(doorZnAnim, reverse);
    }

    void animPlay(Animation anim, bool reverse)
    {
        anim.wrapMode = WrapMode.Once;
        //Debug.Log(anim + " " + state.time + reverse);
        switch (reverse)
        {

            case true:
                foreach (AnimationState state in anim)
                {
                    state.time = 0;
                    state.speed = 1f;
                    anim.Play();

                }
                break;
            case false:
                foreach (AnimationState state in anim)
                {
                    state.time = state.length;
                    state.speed = -1f;

                    anim.Play();

                }
                break;
            default:
                break;
        }
    }

    void GetTurrentComponent(GameObject turrent)
    {
        TurrentHS = turrent.GetComponent<HealthSystem>();
        TurrentRT = turrent.GetComponent<RotateTowards>();
        TurrentSS = turrent.GetComponent<ShootingSystem>();
        TurrentLSS = turrent.GetComponent<LaserShootingSystem>();

    }

    void EnableComponents(bool reverseC)
    {

        if (TurrentSS != null)
        {
            TurrentRT.enabled = reverseC;
            TurrentSS.enabled = reverseC;
        }
        if (TurrentLSS != null)
        {
            TurrentRT.enabled = reverseC;
            TurrentLSS.enabled = reverseC;
        }

    }

}
