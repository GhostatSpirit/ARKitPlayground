using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;
using UnityEngine.EventSystems;

public class ARHitTestNew : MonoBehaviour
{
	public Transform m_HitTransform;

    public bool canSet = false;

    public bool set = false;

    public bool setted = false;

    public GameObject startButton;

	public GameObject turretInitial;

    bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
	{
		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
		if (hitResults.Count > 0) {
			foreach (var hitResult in hitResults) {
				//Debug.Log ("Got hit!");
				m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
				m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
				//Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));

				if(turretInitial){
					turretInitial.SetActive (true);
				}

				return true;
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if (/*Input.touchCount > 0 &&*/ m_HitTransform != null)
		{
			//var touch = Input.GetTouch(0);

			if (setted == false && canSet == true && set == true)
			{
                setted = true;
                set = false;

                if (startButton)
                {
                    startButton.SetActive(true);
                }

				ARPoint point = new ARPoint {
					x = 0.5f,
					y = 0.5f
				};

				// prioritize reults types
				ARHitTestResultType[] resultTypes = {
					ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
					// if you want to use infinite planes use this:
					//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
					// ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
					// ARHitTestResultType.ARHitTestResultTypeFeaturePoint
				}; 

				foreach (ARHitTestResultType resultType in resultTypes)
				{
					if (HitTestWithResultType (point, resultType))
					{
						return;
					}
				}
			}
		}
	}


}

