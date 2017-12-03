using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class TurretArranger : MonoBehaviour {
    private bool DebugOn = false;

    public Transform arrangeCube;
    public List<GameObject> boxes;
    public List<GameObject> turretModels;
   
    public Transform arrangeCamera;
    public Transform targetPlayer;
    public Transform hitParent;
    public GameObject boxModel;
    public int turretType=0;

    public Transform turretInitParent;

    
    
    private FileDumper fd;
    bool isSet;
    bool isUnset;
    int groundMask;
    int shadowMask;
    float yOffset = 0.5f;
    float blockGap = 1f;
    public float range = 100f;
    Vector3 parentScale;
    float parentScalef;
    


    // Use this for initialization
    void Start() {
        groundMask = LayerMask.GetMask("Ground");
        shadowMask = LayerMask.GetMask("ShadowCollider");
        parentScale = hitParent.GetComponent<ARScale>().unitVector;
        parentScalef = hitParent.GetComponent<ARScale>().unit;
        fd = GetComponent<FileDumper>();

        turretInitialize(turretInitParent);
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
            GameObject tmp = Instantiate(boxModel, turretPostion, arrangeCube.rotation);
            tmp.GetComponent<BoxDoorControl>().turrent = turretModels[turretType];
            tmp.transform.parent = hitParent;
            tmp.GetComponent<BasicInfo>().initial(turretType, turretPostion);
            boxes.Add(tmp);
           
        }

        //unset the turret
        isUnset = Input.GetKeyDown(KeyCode.O);
        if (isUnset == true) {
            GameObject topObj= unsetTurret();
            if (topObj != null) {
                Destroy(topObj);
                boxes.Remove(topObj);
               
            }
            
        }

        bool rearrange = Input.GetKeyDown(KeyCode.I);
        if (rearrange == true)
        {
            rearrangeAllTurret();
        }

        bool save = Input.GetKeyDown(KeyCode.U);
        if (save == true) {
            fd.SaveToFile();
        }

        bool load = Input.GetKeyDown(KeyCode.L);
        if (load == true)
        {
            List<int> types=new List<int>();
            List<Vector3> positions=new List<Vector3>();
            fd.LoadFromFile(ref types,ref positions);
            for (int i = 0; i < positions.Count; i++) {
                GameObject tmp = Instantiate(boxModel, positions[i], arrangeCube.rotation);
                tmp.GetComponent<BoxDoorControl>().turrent = turretModels[types[i]];
                tmp.transform.parent = hitParent;
                tmp.GetComponent<BasicInfo>().initial(turretType, positions[i]);
                boxes.Add(tmp);
            }
        }

        
    }

    void turretInitialize(Transform t)
    {
        foreach (Transform i in t)
        {
            Debug.Log("child:"+i.name);
            if (i.tag == "TurretBox")
            {
                Debug.Log("here");
                boxes.Add(i.gameObject);
            }
            else {
                turretInitialize(i);
            }
        }
    }

    void rearrangeAllTurret()
    {
        foreach (GameObject i in boxes)
        {
            Vector3 direction = findDirection(i);
            i.GetComponent<ActiveSetting>().setTurretPosi(direction);
            
        }
        
    }

    bool checkBlockNext(Vector3 objPosition, Vector3 direction) {
        Ray shootRay = new Ray();
        RaycastHit[] shootHit;
        shootRay.origin = objPosition;       
        bool flag = false;
        shootRay.direction = direction;
        shootHit = Physics.RaycastAll(shootRay, range, shadowMask);
        if (shootHit.Length == 0) { return false; }
        foreach (RaycastHit i in shootHit)
        {
            if (Mathf.Approximately((i.point - shootRay.origin).magnitude, blockGap*parentScalef/2))
            {
                if (DebugOn)
                    Debug.Log("something in direction" + i.point + shootRay.direction + (i.point - shootRay.origin).magnitude);
                flag = true;
            }

        }
        
        return flag;
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
        shootRay.origin = objPosition ;
        shootRay.direction = new Vector3(0,1,0);
        shootHit=Physics.RaycastAll(shootRay, range,shadowMask);
        if (shootHit.Length == 0) {
            return new Vector3(0, 1, 0);
            
        }

        //front
        flag = checkBlockNext(objPosition, front);
        if (flag == false) { return front; }
        flag = false;

        //right
        flag = checkBlockNext(objPosition, right) ;
        if (flag == false) { return right; }
        flag = false;

        //-right
        flag = checkBlockNext(objPosition, new Vector3(-right.x, 0, -right.z)) ;
        if (flag == false) { return new Vector3(-right.x, 0, -right.z); }
        flag = false;

        //-front
        flag = checkBlockNext(objPosition, new Vector3(-front.x, 0, -front.z)) ;
        if (flag == false) { return new Vector3(-front.x, 0, -front.z); }
        flag = false;

        return new Vector3(0,0,0);
    }

    GameObject unsetTurret() {
        Vector3 turretPostion = arrangeCube.position;
        GameObject topObj = null;
        float topY = 0;
        int count = 0;
        int index = -1;
        foreach (GameObject i in boxes)
        {
            if (posiEqual(i.transform.position.x, turretPostion.x) && posiEqual(i.transform.position.z, turretPostion.z))
            {
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
            return topObj;
            
        }
        return null;
        

    }

    Vector3 setTurretPostion()
    {
        Vector3 turretPostion = arrangeCube.position;
        GameObject topObj = null;
        float topY = (-1+yOffset)*parentScalef;
        foreach (GameObject i in boxes)
        {

            if (posiEqual(i.transform.position.x,turretPostion.x)&&posiEqual(i.transform.position.z,turretPostion.z) )
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
            //topObj.GetComponent<ActiveSetting>().turretDisablePosi();
        }
        
        turretPostion.y = topY + 1 * parentScalef;
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
            
            arrangeCube.position = new Vector3((float)((int)(hitPoint.x/parentScale.x))*parentScale.x, (float)((int)(hitPoint.y/parentScale.y))*parentScale.y, (float)((int)(hitPoint.z/parentScale.z))*parentScale.z);

        }
    }

    bool posiEqual(float a, float b) {
        if ((a - b <= blockGap*parentScale.x/2 && a - b >= -blockGap*parentScale.x/2)) {
            return true;
        }
        return false;
    }

   
    public void ToggleTurrets()
    {
        if (boxes != null)
        {
            foreach (GameObject turret in boxes)
            {
                //Transform[] childrenEle;
                //childrenEle = turret.transform.GetComponentsInChildren<Transform>();
                foreach (Transform i in turret.transform.GetComponentsInChildren<Transform>())
                {
                    if (DebugOn)
                        Debug.Log(i.name);
                    if (i.tag=="TurretBox")
                    {
                        
                        TurretControl tc = i.GetComponentInChildren<TurretControl>();
                        tc.ToggleRotate();
                        tc.ToggleShoot();
                    }
                }
                
                
            }
        }
    }

}
