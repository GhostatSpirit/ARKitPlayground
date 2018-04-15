using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Attach this script to a transform at position (0, 0, 0)
 * under AR anchor.
 */
public class CubeBuilder : MonoBehaviour {

    public BaseCube activeCube;
    public ARScreenRaycast sr;

    public ARScale arScale;
    float unit = 0.1f;

    public LayerMask cubeLayer;

    Transform anchor {
        get {
            UpdateAnchor();
            return _anchor;
        }
    }
    Transform _anchor;
    Transform _turretAnchor;

    Transform turretAnchor {
        get {
            UpdateAnchor();
            return _turretAnchor;
        }
    }
    BaseCube anchorCube;

    public Material activeMaterial;
    public Material inactiveMaterial;

    public BaseCube coreCube;

    [ReadOnly]
    public bool isCubeCollided = false;
    [ReadOnly]
    public Vector3 neighborDir;

	// Use this for initialization
	void Start () {
        if (arScale) unit = arScale.unit;
	}
	
	// Update is called once per frame
	void Update () {
        ARRaycastHit arhit = sr.arhit;
        
        if(arhit.type != ARRaycastHit.HitType.NoHit)
        {
            Vector3 worldBoxPos = GetWorldTargetPos(arhit);
            anchor.position = worldBoxPos;

            neighborDir = GetNeighborDirection(anchor);
            bool isNeighborToCube = neighborDir != Vector3.zero;
            isCubeCollided = IsCubeCollided(anchor);


            if(isCubeCollided)
            {
                anchor.gameObject.SetActive(false);
            }
            else
            {
                anchor.gameObject.SetActive(true);
                SetRendererMaterial(anchor, isNeighborToCube);

                if(anchorCube is TurretCube)
                {
                    // set pos of the turret hologram
                    Vector3 spaceDir = GetSpaceDirection(anchor);
                    turretAnchor.position = worldBoxPos + spaceDir * unit / 2.0f;
                    turretAnchor.up = spaceDir;
                }

                if(isNeighborToCube)
                {
                    // respawn cube in the unity editor
#if UNITY_EDITOR
                    if(Input.GetMouseButtonDown(0) && 
                       !EventSystem.current.IsPointerOverGameObject())
                    {
                        Instantiate(activeCube.cube, anchor.position, 
                                    anchor.rotation, transform);
                    }
#elif UNITY_STANDALONE
                    if(Input.GetMouseButtonDown(0) && 
                       !EventSystem.current.IsPointerOverGameObject())
                    {
                        Instantiate(activeCube.cube, anchor.position, 
                                    anchor.rotation, transform);
                    }
#endif
                }
            }
            
        }
        else
        {
            anchor.gameObject.SetActive(false);
        }

	}

    public void TryDeleteCube()
    {
        ARRaycastHit arhit = sr.arhit;
        if(arhit.type == ARRaycastHit.HitType.UnityHit)
        {
//            Debug.Log(arhit.hit);
            CubeData cd = arhit.hit.transform.GetComponent<CubeData>();

            if (!cd || cd.data == coreCube) return;
            else
            {
                Destroy(cd.gameObject);
            }
        }
    }

    public void TryPlaceCube()
    {
        bool isNeighborToCube = neighborDir != Vector3.zero;
        if(isNeighborToCube)
        {
            Instantiate(activeCube.cube, anchor.position, anchor.rotation, transform);
        }
    }

    Vector3 GetWorldTargetPos(ARRaycastHit arhit)
    {
        Vector3 localPos = transform.InverseTransformPoint(arhit.point);
        Vector3 localNormal = transform.InverseTransformDirection(arhit.normal);


        Vector3 bottomPos = new Vector3(localPos.x, localPos.y - unit / 2.0f, localPos.z);
        Vector3 targetPos = bottomPos;

        //Debug.Log(string.Format("point: (x:{0:0.######} y:{1:0.######} z:{2:0.######})", 
        //                        arhit.point.x, arhit.point.y, arhit.point.z));
        //Debug.Log(string.Format("normal: (x:{0:0.######} y:{1:0.######} z:{2:0.######})",
        //                        arhit.normal.x, arhit.normal.y, arhit.normal.z));

        targetPos += AlignVector(localNormal) * (unit / 2.0f);
        targetPos = RoundToUnit(targetPos);

        targetPos.y += unit / 2.0f;

        return transform.TransformPoint(targetPos);
    }

    Vector3 AlignVector(Vector3 v)
    {
        v.Normalize();

        float upDot = Vector3.Dot(v, Vector3.up);
        float rightDot = Vector3.Dot(v, Vector3.right);
        float forwardDot = Vector3.Dot(v, Vector3.forward);

        float absUpDot = Mathf.Abs(upDot);
        float absRightDot = Mathf.Abs(rightDot);
        float absForwardDot = Mathf.Abs(forwardDot);

        if (absUpDot >= absRightDot && absUpDot >= absForwardDot)
            return (upDot > 0) ? Vector3.up : -Vector3.up;
        else if (absRightDot >= absUpDot && absRightDot >= absForwardDot)
            return (rightDot > 0) ? Vector3.right : -Vector3.right;
        else
            return (forwardDot > 0) ? Vector3.forward : -Vector3.forward;
    }

    Vector3 RoundToUnit(Vector3 v)
    {
        Vector3 unitPos = new Vector3(v.x / unit, v.y / unit, v.z / unit);
        unitPos.x = Mathf.Round(unitPos.x);
        unitPos.y = Mathf.Round(unitPos.y);
        unitPos.z = Mathf.Round(unitPos.z);

        return new Vector3(unitPos.x * unit, unitPos.y * unit, unitPos.z * unit);
    }

    bool IsCubeCollided(Transform anchor)
    {
        Vector3 halfExtents = arScale.unitVector * 0.9f / 2.0f;
        return Physics.CheckBox(anchor.position, halfExtents, anchor.rotation, cubeLayer);
    }

    // return (0, 0, 0) if no space if found in 6 directions,
    // else, return the direction (in world space) to the space (normalized)
    Vector3 GetSpaceDirection(Transform anchor)
    {
        float dist = unit;
        Vector3 origin = anchor.position;

        Vector3[] dirs = new Vector3[]
        {
             anchor.up,
            -anchor.up,
             anchor.right,
            -anchor.right,
             anchor.forward,
            -anchor.forward
        };

        foreach(var dir in dirs)
        {
            if (!Physics.Raycast(origin, dir, dist, cubeLayer))
                return dir;
        }

        return new Vector3(0, 0, 0);
    }

    Vector3 GetNeighborDirection(Transform anchor)
    {
        float dist = unit;
        Vector3 origin = anchor.position;

        Vector3[] dirs = new Vector3[]
        {
             anchor.up,
            -anchor.up,
             anchor.right,
            -anchor.right,
             anchor.forward,
            -anchor.forward
        };

        foreach (var dir in dirs)
        {
            if (Physics.Raycast(origin, dir, dist, cubeLayer))
                return dir;
        }

        return new Vector3(0, 0, 0);
    }

    void SetRendererMaterial(Transform anchor, bool isActive)
    {
        var renderers = anchor.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
            renderer.material = isActive ? activeMaterial : inactiveMaterial;
    }

    void UpdateAnchor()
    {
        if (anchorCube != activeCube || _anchor == null)
        {
            GameObject newAnchorGO = Instantiate(activeCube.anchor, transform);
            Transform newAnchor = newAnchorGO.transform;
            if (_anchor)
            {
                newAnchor.position = _anchor.position;
                newAnchor.rotation = _anchor.rotation;
                Destroy(_anchor.gameObject);
            }

            TurretCube tcube = activeCube as TurretCube;
            if (tcube == null)
                _turretAnchor = null;
            else
            {
                GameObject turretGO = Instantiate(tcube.turretAnchor, newAnchor.position,
                    newAnchor.rotation, newAnchor);
                _turretAnchor = turretGO.transform;
            }

            _anchor = newAnchor;
            anchorCube = activeCube;
        }
    }
}
