using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurretArranger : MonoBehaviour {
    public Transform arrangeCube;
    public List<GameObject> turrets;
    public GameObject turretModel;
    public Transform arrangeCamera;
    public Transform targetPlayer;

    bool isSet;
    bool isUnset;
    int groundMask;
    float yOffset = 0.5f;
    public float range = 100f;

    // Use this for initialization
    void Start() {
        groundMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update() {
        //config the pointed location
        ArrangeBoxMotion();

        //set the turret
        isSet = Input.GetKeyDown(KeyCode.P);
        if (isSet == true)
        {
            Vector3 turretPostion = setTurretPostion();
            GameObject tmp = Instantiate(turretModel, turretPostion, arrangeCube.rotation);
            tmp.GetComponent<RotateTowards>().target = targetPlayer;
            turrets.Add(tmp);
            updateOtherTurret();
        }

        //unset the turret
        isUnset = Input.GetKeyDown(KeyCode.O);
        if (isUnset == true) {
            unsetTurret();
            updateOtherTurret();
        }

        bool rearrange = Input.GetKeyDown(KeyCode.I);
        if (rearrange == true)
        {
            rearrangeAllTurret();
        }

    }

    void updateOtherTurret() {
        foreach (GameObject i in turrets)
        {
            if (!i.GetComponent<ActiveSetting>().isTurretActive()) {
                Vector3 direction=findDirection(i);
                i.GetComponent<ActiveSetting>().setTurretPosi(direction);
            }
        }
    }
    void rearrangeAllTurret()
    {
        foreach (GameObject i in turrets)
        {
            
                Vector3 direction = findDirection(i);
                i.GetComponent<ActiveSetting>().setTurretPosi(direction);
            
        }
        //foreach (GameObject i in turrets)
        //{

        //    Vector3 direction = findDirection(i);
        //    i.GetComponent<ActiveSetting>().setTurretPosi(direction);

        //}
    }

    Vector3 findDirection(GameObject obj) {
        Vector3 front;
        Vector3 right;
        bool flag = false;
        Vector3 objPosition = obj.transform.position;
        if (Mathf.Abs(objPosition.x) >= Mathf.Abs(objPosition.z))
        {
            if (objPosition.x >= 0) { front = new Vector3(1, 0, 0); }
            else { front = new Vector3(-1, 0, 0); }
            if (objPosition.z >= 0) { right = new Vector3(0, 0, 1); }
            else { right = new Vector3(0, 0, -1); }
        }
        else {
            if (objPosition.x >= 0) { right = new Vector3(1, 0, 0); }
            else { right = new Vector3(-1, 0, 0); }
            if (objPosition.z >= 0) { front = new Vector3(0, 0, 1); }
            else { front = new Vector3(0, 0, -1); }
        }

        Ray shootRay = new Ray();
        RaycastHit[] shootHit;
        shootRay.origin = obj.transform.position;

        //up
        flag = false;
        shootRay.origin = objPosition + new Vector3(0.49f, 0, 0.49f);
        shootRay.direction = new Vector3(0,1,0);
        shootHit=Physics.RaycastAll(shootRay, range);
        if (shootHit.Length == 0) {
            shootRay.origin = objPosition + new Vector3(-0.49f, 0, -0.49f);
            shootRay.direction = new Vector3(0, 1, 0);
            shootHit = Physics.RaycastAll(shootRay, range);
            if (shootHit.Length == 0)
            {
                return new Vector3(0, 1, 0);
            }
        }
        
        


        shootRay.origin = objPosition;
        //front
        flag = false;
        shootRay.direction = front;
        shootHit = Physics.RaycastAll(shootRay, range);
        if (shootHit.Length == 0) { return shootRay.direction; }
        foreach (RaycastHit i in shootHit)
        {
            if (isAlmostEqual((i.point - objPosition).magnitude, 0.5f))
            {
                Debug.Log("something front" + i.point + shootRay.direction + (i.point - objPosition).magnitude);
                flag = true;
            }
            //Debug.Log((i.point - objPosition).magnitude);
            //else { return front; }

        }
        if (flag == false) { return shootRay.direction; }
        flag = false;

        //right
        shootRay.direction = right;
        shootHit = Physics.RaycastAll(shootRay, range);
        if (shootHit.Length == 0) { return shootRay.direction; }
        foreach (RaycastHit i in shootHit)
        {
            if (isAlmostEqual((i.point - objPosition).magnitude, 0.5f))
            {
                Debug.Log("something right" + i.point + shootRay.direction + (i.point - objPosition).magnitude);
                flag = true;
            }
            //else { return right; }
            //Debug.Log((i.point - objPosition).magnitude);
        }
        if (flag == false) { return shootRay.direction; }
        flag = false;

        //-right
        shootRay.direction = new Vector3(-right.x, 0, -right.z);
        shootHit = Physics.RaycastAll(shootRay, range);
        if (shootHit.Length == 0) { Debug.Log("direction:"+shootRay.direction); return shootRay.direction; }
        foreach (RaycastHit i in shootHit)
        {
            if (isAlmostEqual((i.point - objPosition).magnitude, 0.5f))
            {
                Debug.Log("something -right" + i.point+shootRay.direction+ (i.point - objPosition).magnitude);
                flag = true;
            }
           // else { return shootRay.direction; }
            //Debug.Log((i.point - objPosition).magnitude);
        }
        if (flag == false) { Debug.Log("direction:" + shootRay.direction); return shootRay.direction; }
        flag = false;

        //-front
        shootRay.direction = new Vector3(-front.x, 0, -front.z);
        shootHit = Physics.RaycastAll(shootRay, range);
        if (shootHit.Length == 0) { return shootRay.direction; }
        foreach (RaycastHit i in shootHit)
        {
            if (isAlmostEqual((i.point - objPosition).magnitude, 0.5f))
            {
                Debug.Log("something -front" + i.point + shootRay.direction + (i.point - objPosition).magnitude);
                flag = true;
            }
           // else { return shootRay.direction; }
            //Debug.Log((i.point - objPosition).magnitude);
        }
        if (flag == false) { return shootRay.direction; }
        flag = false;


        
        return new Vector3(0,0,0);
    }

    void unsetTurret() {
        Vector3 turretPostion = arrangeCube.position;
        GameObject topObj = null;
        float topY = 0;
        int count = 0;
        int index = -1;
        foreach (GameObject i in turrets)
        {
            if ((i.transform.position.x - turretPostion.x<=0.5 && i.transform.position.x - turretPostion.x >= -0.5) && (i.transform.position.z - turretPostion.z <= 0.5 && i.transform.position.z - turretPostion.z >= -0.5))
            {
                Debug.Log("find someone");
                if (i.transform.position.y > topY)
                {
                    topObj = i;
                    topY = i.transform.position.y;
                    index = count;


                }
            }
            count++;
        }
        if (index != -1) {
            Destroy(topObj, 0f);
            turrets.Remove(topObj);
        }
        

    }
    Vector3 setTurretPostion()
    {
        Vector3 turretPostion = arrangeCube.position;
        GameObject topObj = null;
        float topY = -1+yOffset;
        foreach (GameObject i in turrets)
        {

            if ((i.transform.position.x - turretPostion.x <= 0.5 && i.transform.position.x - turretPostion.x >= -0.5) && (i.transform.position.z - turretPostion.z <= 0.5 && i.transform.position.z - turretPostion.z >= -0.5))
            {
                if (i.transform.position.y > topY)
                {
                    topObj = i;
                    topY = i.transform.position.y;

                }
            }
        }
        if (topObj != null)
        {
            topObj.GetComponent<ActiveSetting>().turretDisablePosi();

        }
        turretPostion.y = topY + 1;
        //GameObject tmp = Instantiate(turretModel, turretPostion, arrangeCube.rotation);
        //tmp.GetComponent<RotateTowardsT>().target = targetPlayer;
       // turrets.Add(tmp);
        return turretPostion;
    }

    void ArrangeBoxMotion() {
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

    bool posiEqual(float a, float b) {
        if ((a - b <= 0.5 && a - b >= -0.5)) {
            return true;
        }
        return false;
    }

    bool isAlmostEqual(float a,  float b) {
        if ((a - b <= 0.05 && a - b >= -0.05))
        {
            return true;
        }
        return false;
    }

}
