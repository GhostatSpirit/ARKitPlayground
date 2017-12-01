using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileDumper : MonoBehaviour {
    public string path;
    private TurretArranger TA;
    private List<GameObject> boxes;

    private class Turret
    {
        public int type;
        public Vector3 position;
    };
    // Use this for initialization
    void Start () {
        TA = GetComponent<TurretArranger>();
        boxes = TA.boxes;
        path = @"MyTest.txt";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void saveToFile()
    {
        //FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        
        //StreamWriter sw = new StreamWriter(fs);


        //string turretString = "";
        //foreach (GameObject turret in turrets)
        //{
        //    turretString = "";
        //    turretString+=turret.
        //}
    }
}
