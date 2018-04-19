using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneLoader : MonoBehaviour {

    public string levelSceneName = "LevelNew";
    public ActiveBastionFile activeBastionFile;

	public void LoadScene(BastionData bastionData)
    {
        activeBastionFile.bastionData = bastionData;
        SceneManager.LoadScene(levelSceneName);
    }
}
