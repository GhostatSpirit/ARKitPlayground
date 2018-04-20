using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARButtonDisabler : MonoBehaviour {
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
#if UNITY_IOS
        button.interactable = false;
#endif
    }



}
