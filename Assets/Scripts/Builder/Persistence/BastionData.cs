using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct CubeSaveData
{
    public int cubeId;
    public Vector3 position;
}

[System.Serializable]
public struct BastionSaveData
{
    public List<CubeSaveData> cubes;
}

[CreateAssetMenu(fileName = "BastionData", menuName = "CubeData/Bastion Data")]
public class BastionData : ScriptableObject {

    public string fileName = "bastion02.json";
    public string levelPath = "Levels/";

    public bool isBuilder = false;

    public CubeList cubeList;
    // public BastionSaveData data;

    List<BaseCube> cubeDataList {
        get {
            if (cubeList)
                return cubeList.cubes;
            else
                return new List<BaseCube>();
        }
    }

    Dictionary<BaseCube, int> cubeIdDict;

    private void OnEnable()
    {
        cubeIdDict = new Dictionary<BaseCube, int>();
        for (int id = 0; id < cubeDataList.Count; ++id)
        {
            cubeIdDict.Add(cubeDataList[id], id);
        }
    }

    public void Save(Transform cubeParent)
    {

        var cubeSaves = new List<CubeSaveData>();
        var cubeDatas = cubeParent.GetComponentsInChildren<CubeData>();
        var rigidbodys = cubeParent.GetComponentsInChildren<Rigidbody>();

        // stop all cubes from falling
        foreach (var rb in rigidbodys)
        {
            rb.isKinematic = true;
        }

        foreach (var cd in cubeDatas)
        {
            CubeSaveData sd;

            int cubeId = -1;
            if (cubeIdDict.TryGetValue(cd.data, out cubeId))
            {
                sd.cubeId = cubeIdDict[cd.data];
                sd.position = cubeParent.transform.InverseTransformPoint(cd.transform.position);
                cubeSaves.Add(sd);
            }
            else
            {
                Debug.Log("Warning: Saving skips cube [" + cd.name + "].");
            }

        }

        string savePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("Saving bastion to: " + savePath);

        BastionSaveData bsd = new BastionSaveData
        {
            cubes = cubeSaves
        };

        File.WriteAllText(savePath, JsonUtility.ToJson(bsd, true));

        foreach (var rb in rigidbodys)
        {
            rb.isKinematic = false;
        }
    }

    public void Load(Transform cubeParent)
    {
        string dataAsJson = "";
        if (isBuilder)
        {
            // read bastion from persistentDataPath
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
                dataAsJson = File.ReadAllText(filePath);
            else
            {
                Debug.Log("Read Bastion Json from \"" + filePath + "\" failed.");
                return;
            }
        }
        else
        {
            // read bastion from resource folder
            string filePath = Path.Combine(levelPath, fileName);
            Debug.Log(filePath);
            TextAsset textAsset = Resources.Load(filePath, typeof(TextAsset)) as TextAsset;
            if(textAsset != null)
            {
                dataAsJson = textAsset.text;
            }
            else
            {
                Debug.Log("Read Bastion Text Asset from \"" + filePath + "\" failed.");
                return;
            }
        }

        BastionSaveData bastion = JsonUtility.FromJson<BastionSaveData>(dataAsJson);

        List<Rigidbody> bodys = new List<Rigidbody>(bastion.cubes.Count);
        Quaternion parentRot = cubeParent.rotation;

        foreach (CubeSaveData sd in bastion.cubes)
        {
            Vector3 worldPos = cubeParent.TransformPoint(sd.position);

            BaseCube data = TryGetCubeData(sd.cubeId);
            if (data == null) continue;

            GameObject cubeGO =
                Instantiate(data.cube, worldPos, parentRot, cubeParent);
            Rigidbody body = cubeGO.GetComponent<Rigidbody>();
            if (body)
            {
                body.isKinematic = true;
                bodys.Add(body);
            }
        }

        foreach (var body in bodys)
            body.isKinematic = false;
    }

    BaseCube TryGetCubeData(int id)
    {
        if (id < 0 || id >= cubeDataList.Count)
            return null;
        else
            return cubeDataList[id];
    }

}
