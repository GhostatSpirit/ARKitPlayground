﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class ButtonEvent : UnityEvent<Button>
{
}


public class ButtonInterface : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    public ButtonEvent OnButtonClick;
    public ButtonEvent OnButtonRelease;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ClickForwarder);

        if (OnButtonRelease == null)
            OnButtonRelease = new ButtonEvent();
    }

    void ClickForwarder()
    {
        OnButtonClick.Invoke(button);
        // Debug.Log(gameObject.name + "button pressed!");
    }

    // OnPointerDown is also required to receive OnPointerUp callbacks
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if(!eventData.dragging)
        {
            OnButtonRelease.Invoke(button);
            // Debug.Log(gameObject.name + " released!");

        }
    }


}
