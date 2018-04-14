using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseCube", menuName = "CubeData/BaseCube")]
public class BaseCube : ScriptableObject {

    public GameObject anchor;
    public GameObject cube;

    public int health = 100;
}