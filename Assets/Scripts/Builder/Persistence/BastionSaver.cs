using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public struct CubeSaveData
{
    public BaseCube cube;
    public Vector3 position;
}

[System.Serializable]
public struct BastionSaveData
{
    public List<CubeSaveData> cubes;
}

public class BastionSaver : MonoBehaviour {

    public Transform cubeParent;
    public string fileName = "bastion00.json";


    public void Save()
    {
        var cubeSaves = new List<CubeSaveData>();
        var cubeDatas = cubeParent.GetComponentsInChildren<CubeData>();
        var rigidbodys = cubeParent.GetComponentsInChildren<Rigidbody>();

        // stop all cubes from falling
        foreach(var rb in rigidbodys)
        {
            rb.isKinematic = true;
        }

        foreach(var cd in cubeDatas)
        {
            CubeSaveData sd;
            sd.cube = cd.data;
            sd.position = cubeParent.transform.InverseTransformPoint(cd.transform.position);
            cubeSaves.Add(sd);
        }

        string savePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("Saving bastion to: " + savePath);

        BastionSaveData bsd = new BastionSaveData();
        bsd.cubes = cubeSaves;

        File.WriteAllText(savePath, JsonUtility.ToJson(bsd, true));

        foreach (var rb in rigidbodys)
        {
            rb.isKinematic = false;
        }
    }

    public void Load()
    {

    }
}
