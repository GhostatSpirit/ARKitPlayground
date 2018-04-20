using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class textForTest : MonoBehaviour {

    Text text;

    public AimScale AS;

    public bool displayDistance;

    public bool displayDistanceLimit;

    public bool displayAngle;

    public bool displatAngleLimit;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (displayDistance)
        {
            text.text = AS.movingDistance.ToString();
        }
        else if(displayAngle)
        {
            text.text = AS.rotationAngle.ToString();
        }
        else if (displatAngleLimit)
        {
            text.text = AS.movingScaleActiveAngle.ToString();
        }
        else if (displayDistanceLimit)
        {
            text.text = AS.movingScaleActiveDistance.ToString();
        }

	}
}
