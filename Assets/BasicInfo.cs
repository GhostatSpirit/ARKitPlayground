using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInfo : MonoBehaviour {
    private int type;
    private Vector3 initialPosi;

    public BasicInfo() {
        ;
    }

    public void initial(int t, Vector3 i) {
        type = t; initialPosi = i;
    }

    public string GetInfo() {
        string str="";
        str += type.ToString();
        str += " ";
        str += initialPosi.x.ToString()+" ";
        str += initialPosi.y.ToString()+" ";
        str += initialPosi.z.ToString();
        return str;
    }
	
}
