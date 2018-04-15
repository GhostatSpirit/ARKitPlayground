using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeList", menuName = "CubeData/Cube List")]
public class CubeList : ScriptableObject {

    public string displayName = "Cube List";
    public List<BaseCube> cubes;
	
}
