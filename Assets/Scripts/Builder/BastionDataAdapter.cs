using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BastionDataAdapter : MonoBehaviour {

    public Transform bastionRoot;
    // public ActiveBastionFile activeBastionFile;

    public void Load()
    {
        if (ActiveBastionMeta.bastionData)
        {
            var data = ActiveBastionMeta.bastionData;
            data.Load(bastionRoot);
        }
    }

    public void Save()
    {
        if (ActiveBastionMeta.bastionData)
        {
            var data = ActiveBastionMeta.bastionData;
            data.Save(bastionRoot);
        }
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
        Load();
    }
}
