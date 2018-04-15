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

    public CubeBuilder cubeBuilder;

    public CubeList cubeList;

    List<BaseCube> cubes {
        get {
            return cubeList.cubes;
        }
    }

    //Dictionary<Button, BaseCube> button2Cube;
    [SerializeField]
    Button activeButton;

    public void GenerateButtons()
    {

        foreach (var cube in cubes)
        {

            // instantiate cube prefabs
            GameObject cbuttonGO = Instantiate(button, buttonParent);
            cbuttonGO.name = cube.displayName + "CubeButton";

            // attach cube data
            CubeData cd = cbuttonGO.GetComponent<CubeData>();
            cd.data = cube;


            Button cbutton = cbuttonGO.GetComponent<Button>();
            var spriteState = cbutton.spriteState;
            spriteState.disabledSprite = cube.builderSprite.normal;
            spriteState.highlightedSprite = cube.builderSprite.normal;
            spriteState.pressedSprite = cube.builderSprite.pressed;

            // add to dict
            //button2Cube.Add(cbutton, cube);

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
                cubeBuilder.activeCube = cube;
            }

        }
    }

    void SetSelectedSprites(Button button)
    {
        Sprite selected = button.GetComponent<CubeData>().data.builderSprite.selected;

        var spriteState = button.spriteState;
        spriteState.highlightedSprite = selected;
        button.spriteState = spriteState;
        
        button.GetComponent<Image>().sprite = selected;
    }

    void ResetNormalSprites(Button button)
    {
        Sprite normal = button.GetComponent<CubeData>().data.builderSprite.normal;

        var spriteState = button.spriteState;
        spriteState.highlightedSprite = normal;
        button.spriteState = spriteState;

        button.GetComponent<Image>().sprite = normal;
    }

    void SetActiveCube(Button button)
    {
        BaseCube newActive = button.GetComponent<CubeData>().data;
        if(newActive)
        {
            cubeBuilder.activeCube = newActive;
        }
        if(activeButton)
            ResetNormalSprites(activeButton);
        activeButton = button;
    }
	

}
