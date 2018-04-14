using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuilderSprite
{
    public Sprite normal;
    public Sprite pressed;
    public Sprite selected;
}

[CreateAssetMenu(fileName = "BaseCube", menuName = "CubeData/BaseCube")]
public class BaseCube : ScriptableObject {
    public string displayName;

    public GameObject anchor;
    public GameObject cube;

    public int health = 100;

    public BuilderSprite builderSprite;
}