using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectRender : MonoBehaviour {

    MeshRenderer MR;

    Vector4 randomVec4 = new Vector4();

    Color targetColor;

    [HideInInspector]
    public bool active = false;

    Color originColor;

	// Use this for initialization
	void Start () {
        MR = GetComponent<MeshRenderer>();
        targetColor = new Color(0, 0, 0, 0);
        originColor = MR.material.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            active = !active;
        }


        if(active == false)
        {
            targetColor = new Color(0.3f, 0.3f, 0.3f, 0);

            if (MR.material.color != targetColor)
            {
                MR.material.color = Color.Lerp(MR.material.color, targetColor, Time.deltaTime);
            }
        }

        else if (active == true)
        {
            targetColor = originColor;
            if (MR.material.color != targetColor)
            {
                MR.material.color = Color.Lerp(MR.material.color, targetColor, Time.deltaTime);
            }
        }

    }
}
