using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

    AsyncOperation async;

    int NextSceneNum = 0;

    Image image;

    public LoadSceneParam LSP; 

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        NextSceneNum = LSP.targetSceneNum;
        StartCoroutine(SceneLoad());
    }
	
	// Update is called once per frame
	void Update () {
        if(async != null)
        {
            image.fillAmount = async.progress / (float) 0.9;
            if (async.progress >= 0.85)
            {
                ActivatedScene();
            }
        }
        
	}

    public void TransitionScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum, LoadSceneMode.Single);
    }

    public void TransitionSceneAsync()
    {
        ActivatedScene();
    }

    public void ActivatedScene()
    {
        async.allowSceneActivation = true;
    }

    IEnumerator SceneLoad()
    {
        yield return new WaitForEndOfFrame();
        async = SceneManager.LoadSceneAsync(NextSceneNum);
        async.allowSceneActivation = false;
        yield return async;
    }
}
