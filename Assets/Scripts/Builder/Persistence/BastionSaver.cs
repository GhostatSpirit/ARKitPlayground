using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class BastionSaver : MonoBehaviour {

    public Transform cubeParent;


    public ActiveBastionFile activeBastionFile;
    public string defaultFileName = "bastion00.json";

    public bool loadOnSceneLoaded = false;

    private string fileName {
        get {
            //if (activeBastionFile && activeBastionFile.fileName != "")
            //    return activeBastionFile.fileName;
            //else
            //    return defaultFileName;
            return ActiveBastionMeta.fileName;
        }
    }

    public CubeList cubeList;

    List<BaseCube> cubeDataList {
        get {
            return cubeList.cubes;
        }
    }
    Dictionary<BaseCube, int> cubeIdDict;

    public void Start()
    {
        InitCubeIdDict();
    }

    public void InitCubeIdDict()
    {
        cubeIdDict = new Dictionary<BaseCube, int>();
        for (int id = 0; id < cubeDataList.Count; ++id)
        {
            cubeIdDict.Add(cubeDataList[id], id);
        }
    }

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

            int cubeId = -1;
            if(cubeIdDict.TryGetValue(cd.data, out cubeId))
            {
                sd.cubeId = cubeIdDict[cd.data];
                sd.position = cubeParent.transform.InverseTransformPoint(cd.transform.position);
                cubeSaves.Add(sd);
            }
            else
            {
                Debug.Log("Warning: Saving skips cube [" + cd.name +"].");
            }

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
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            BastionSaveData bastion = JsonUtility.FromJson<BastionSaveData>(dataAsJson);

            List<Rigidbody> bodys = new List<Rigidbody>(bastion.cubes.Count);
            Quaternion parentRot = cubeParent.rotation;

            foreach(CubeSaveData sd in bastion.cubes)
            {
                Vector3 worldPos = cubeParent.TransformPoint(sd.position);

                BaseCube data = TryGetCubeData(sd.cubeId);
                if (data == null) continue;   

                GameObject cubeGO =
                    Instantiate(data.cube, worldPos, parentRot, cubeParent);
                Rigidbody body = cubeGO.GetComponent<Rigidbody>();
                if(body)
                {
                    body.isKinematic = true;
                    bodys.Add(body);
                }
            }

            foreach (var body in bodys)
                body.isKinematic = false;
        }
    }

    BaseCube TryGetCubeData(int id)
    {
        if (id < 0 || id >= cubeDataList.Count)
            return null;
        else
            return cubeDataList[id];
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadOnSceneLoaded)
        {
            Load();
        }
    }
}
