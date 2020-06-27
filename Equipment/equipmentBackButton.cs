using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class equipmentBackButton : MonoBehaviour
{
    [Header("Game objects")]
    [SerializeField]private GameObject pauseGameObject;
    [SerializeField] private GameObject equipmentWindow;


    [Header("Lean Tween")]
    [SerializeField]private LeanTweenType easeType;

    [Header("Rect transform")]
    [SerializeField]private RectTransform equipment, description;

    [Header("Script")]
    [SerializeField] private equipmentMenu equipMenu;
    
    RectTransform[] oldChildrenPauseMenuPos, newChildrenPauseMenuPos;

    Vector3[] oldPauseMenuPos, oldPauseMenuScale;
    PauseStateMenu pauseState;
    LTDescr equipmentLT, descriptionLT;
    equipmentButtons equipmentButton;
    itemSelected itemSelect;
    SoundManager sfx;

    private void Awake()
    {
        this.pauseState = GameManager.instance.pauseState;
        this.itemSelect = GameManager.instance.itemSelect;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        equipmentButton = GetComponent<equipmentButtons>();
        oldChildrenPauseMenuPos = GetComponentsInChildren<RectTransform>();
        newChildrenPauseMenuPos = GetComponentsInChildren<RectTransform>();
        oldPos();
    }

    public void backToPauseMenu()
    {
        if (pauseState.equipState != PauseStateMenu.EquipmentState.NONE && pauseState.itemStateButton == "")
        {
            sfx.playPauseTypeBackButton();
            equipmentLT = LeanTween.moveX(equipment, -1936f, 0.2f).setEase(easeType);
            equipmentLT.setOnComplete(stopLeanTween);
        }
        else if ( pauseState.equipState == PauseStateMenu.EquipmentState.NONE && pauseState.itemStateButton == "")
        {
            sfx.playPauseTypeBackButton();
            newPos();
            pauseState.getsetPauseMenuState = PauseStateMenu.PauseMenuState.PAUSEMENU;
            gameObject.SetActive(false);
            pauseGameObject.SetActive(true);
        }

        if (pauseState.itemStateButton == "item selected")
        {
            sfx.playPauseTypeBackButton();
            descriptionLT = LeanTween.moveX(description, -1050f, 0f).setEase(easeType); ;
            descriptionLT.setOnComplete(stopDescriptionLeanTween);
        }
    }

    private void stopLeanTween()
    {

        //going back to equipment window
        if(pauseState.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            equipmentButton.offenseImage.color = equipmentButton.buttonUnchooseColorEquipment;
            equipmentButton.offenseText.color = equipmentButton.equipmentTextUnchooseColor;
        }
        else if(pauseState.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            equipmentButton.defenseImage.color = equipmentButton.buttonUnchooseColorEquipment;
            equipmentButton.defenseText.color = equipmentButton.equipmentTextUnchooseColor;
        }
        else if(pauseState.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            equipmentButton.accessoryImage.color = equipmentButton.buttonUnchooseColorEquipment;
            equipmentButton.accessoryText.color = equipmentButton.equipmentTextUnchooseColor;
        }
        equipment.gameObject.SetActive(false);
        pauseState.equipState = PauseStateMenu.EquipmentState.NONE;



        //removing items on item panel window
        foreach(Transform child in equipmentWindow.transform)
        {   
            Destroy(child.gameObject);
        }
        equipmentButton.DisableEquipmentButtons();
    }

    private void stopDescriptionLeanTween() 
    {
        //going back to item panel window
        if (pauseState.itemStateButton == "item selected")
        {
            itemSelect.setItemSelected(null);

            description.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            pauseState.selectedButton = null;
            pauseState.itemStateButton = "";
            equipMenu.resetCharacterStatsVisual();
        }
    }

    private void oldPos()
    {
        for(int a = 0; a <= oldChildrenPauseMenuPos.Length; a++)
        {
            oldPauseMenuPos = new Vector3[a];
            oldPauseMenuScale = new Vector3[a];
        }
        for(int a = 0; a < oldChildrenPauseMenuPos.Length; a++)
        {
            oldPauseMenuPos[a] = new Vector3(oldChildrenPauseMenuPos[a].localPosition.x,oldChildrenPauseMenuPos[a].localPosition.y,oldChildrenPauseMenuPos[a].localPosition.z);
            oldPauseMenuScale[a] = new Vector3(oldChildrenPauseMenuPos[a].localScale.x,oldChildrenPauseMenuPos[a].localScale.y,oldChildrenPauseMenuPos[a].localScale.z);
        }
    }

    private void newPos()
    {
        for(int a = 0; a < newChildrenPauseMenuPos.Length; a++)
        {
            newChildrenPauseMenuPos[a].localPosition = new Vector3(oldPauseMenuPos[a].x,oldPauseMenuPos[a].y,oldPauseMenuPos[a].z);
            newChildrenPauseMenuPos[a].localScale = new Vector3(oldPauseMenuScale[a].x,oldPauseMenuScale[a].y,oldPauseMenuScale[a].z);
        }
    }
}
