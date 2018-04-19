using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildSceneLoader : MonoBehaviour {

    public string fileName = "bastion01.json";
    public ActiveBastionFile activeBastionFile;
    public string builderSceneName = "BuilderTest";

    public void LoadBuildScene()
    {
        activeBastionFile.fileName = fileName;
        SceneManager.LoadScene(builderSceneName);
    }
}
