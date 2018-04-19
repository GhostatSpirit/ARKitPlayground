using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSimple : MonoBehaviour {

    public LoadSceneParam LSP;

    public int loadingSceneIndex = 2;

    public void TransitionScene()
    {
        SceneManager.LoadScene(loadingSceneIndex, LoadSceneMode.Single);
    }

    public void TransitionNew(int sceneIndex)
    {
        LSP.targetSceneNum = sceneIndex;
        SceneManager.LoadScene(loadingSceneIndex, LoadSceneMode.Single);
    }
}
