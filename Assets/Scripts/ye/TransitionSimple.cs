using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSimple : MonoBehaviour {

    public LoadSceneParam LSP;

    public int loadingSceneIndex = 2;

    public ActiveBastionFile activeBastionFile;

    public void TransitionSceneWithoutLoad(int targetSceneIndex)
    {
        SceneManager.LoadScene(targetSceneIndex, LoadSceneMode.Single);
    }

    public void TransitionNew(int sceneIndex)
    {
        Debug.Log(loadingSceneIndex);
        LSP.targetSceneNum = sceneIndex;    
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void TransitionSetBastionData(BastionData bastionData)
    {
        if (activeBastionFile)
        {
            activeBastionFile.bastionData = bastionData;
        }
    }

    public void TransitionSetBuildName(string fileName)
    {
        if (activeBastionFile)
        {
            activeBastionFile.fileName = fileName;
        }
        //SceneManager.LoadScene(builderSceneName);
    }
}
