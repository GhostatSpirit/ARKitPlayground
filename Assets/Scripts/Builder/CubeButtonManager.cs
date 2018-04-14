using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[ExecuteInEditMode]
public class CubeButtonManager : MonoBehaviour {

    public GameObject button;
    public Transform buttonParent;

    public CubePlacer cubePlacer;
    public List<BaseCube> cubes;

    Dictionary<Button, BaseCube> button2Cube;
    Button activeButton;

    public void GenerateButtons()
    {
        button2Cube = new Dictionary<Button, BaseCube>();

        if (cubes == null)
            cubes = new List<BaseCube>();

        foreach (var cube in cubes)
        {

            // instantiate cube prefabs
            GameObject cbuttonGO = Instantiate(button, buttonParent);
            cbuttonGO.name = cube.displayName + "CubeButton";

            Button cbutton = cbuttonGO.GetComponent<Button>();
            var spriteState = cbutton.spriteState;
            spriteState.disabledSprite = cube.builderSprite.normal;
            spriteState.highlightedSprite = cube.builderSprite.normal;
            spriteState.pressedSprite = cube.builderSprite.pressed;

            // add to dict
            button2Cube.Add(cbutton, cube);

            // add a button release callback
            ButtonInterface bri = cbuttonGO.GetComponent<ButtonInterface>();

#if UNITY_EDITOR
            UnityEditor.Events.UnityEventTools.AddPersistentListener<Button>
                (bri.OnButtonRelease, new UnityAction<Button>(SetSelectedSprites));
#else
            bri.OnButtonRelease.AddListener(SetSelectedSprites);
#endif

            // add a button click callback
#if UNITY_EDITOR
            UnityEditor.Events.UnityEventTools.AddPersistentListener<Button>
                (bri.OnButtonClick, new UnityAction<Button>(SetActiveCube));
#else
            bri.OnButtonClick.AddListener(SetActiveCube);
#endif


            Text cText = cbuttonGO.GetComponentInChildren<Text>();
            cText.text = cube.displayName;

            if (activeButton == null)
            {
                activeButton = cbutton;
                cubePlacer.activeCube = cube;
            }

        }
    }

    void SetSelectedSprites(Button button)
    {
        Sprite pressed = button2Cube[button].builderSprite.pressed;

        var spriteState = button.spriteState;
        spriteState.highlightedSprite = pressed;
        button.spriteState = spriteState;
        
        button.GetComponent<Image>().sprite = pressed;
    }

    void ResetNormalSprites(Button button)
    {
        Sprite normal = button2Cube[button].builderSprite.normal;

        var spriteState = button.spriteState;
        spriteState.highlightedSprite = normal;
        button.spriteState = spriteState;

        button.GetComponent<Image>().sprite = normal;
    }

    void SetActiveCube(Button button)
    {
        BaseCube newActive = button2Cube[button];
        if(newActive)
        {
            cubePlacer.activeCube = newActive;
        }

        ResetNormalSprites(activeButton);
        activeButton = button;
    }
	

}
