using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class equipmentButtons : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] public Color buttonChooseColorEquipment;
    [SerializeField] public Color buttonUnchooseColorEquipment;
    [SerializeField] public Color equipmentTextChooseColor;
    [SerializeField] public Color equipmentTextUnchooseColor;
    [SerializeField] private Color unselectedTextColor;

    [Header("Text Mesh Pro")]
    [SerializeField] public TextMeshProUGUI offenseText;
    [SerializeField] public TextMeshProUGUI defenseText;
    [SerializeField] public TextMeshProUGUI accessoryText;
    [SerializeField] public TextMeshProUGUI className;

    [Header("Images")]
    [SerializeField] public Image offenseImage;
    [SerializeField] public Image defenseImage;
    [SerializeField] public Image accessoryImage;
    [SerializeField] private Image classIcon;

    [Header("Game Objects")]
    [SerializeField] private GameObject equipmentWindow;
    [SerializeField] private GameObject ScriptStart,nextCharacter,previousCharacter, itemPanelObject;
    [SerializeField] private GameObject descriptionWindow;

    [Header("Camera Equipment Position")]
    [SerializeField] private float mainCharCamPosX;
    [SerializeField] private float friendOneCamPosX,friendTwoCamPosX,friendThreeCamPosX;

    [Header("Graphic Raycaster Equipment Buttons")]
    [SerializeField] private GraphicRaycaster[] equipmentButtonGR;

    private SoundManager sfx;

    [Header("Game Scripts")]
    [SerializeField] private equipmentMenu equipMenu;

    GameObject equipmentCamera;
    public PauseStateMenu state;
    Party party;
    partyMembers PartyMembers;
    itemSelected itemSelect;
    checkGender isBoy;


    moveTransformPosition movePosX;

    int partyCount;

    private void OnEnable() => state.selectedButtonOnChange += onSelectedButtonChange;
    private void OnDisable() => state.selectedButtonOnChange -= onSelectedButtonChange;

    private void Awake()
    {
        this.state = GameManager.instance.pauseState;
        this.itemSelect = GameManager.instance.itemSelect;
        this.party = GameManager.instance.party;
        this.PartyMembers = GameManager.instance.PartyMembers;
        this.isBoy = GameManager.instance.gender;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        partyCount = 1;
        equipmentWindow.SetActive(false);

        //move position camera
        equipmentCamera = GameObject.FindGameObjectWithTag("EquipmentCamera");
        movePosX = equipmentCamera.GetComponent<moveTransformPosition>();

        resetEquipmentCameraPosition();
        checkCharacter();
        checkNextPreviousButtons();

        //Text
        offenseText.text = "";
        defenseText.text = "";
        accessoryText.text = "";
        offenseText.color = equipmentTextUnchooseColor;
        defenseText.color = equipmentTextUnchooseColor;
        accessoryText.color = equipmentTextUnchooseColor;

        //image color
        offenseImage.color = buttonUnchooseColorEquipment;
        defenseImage.color = buttonUnchooseColorEquipment;
        accessoryImage.color = buttonUnchooseColorEquipment;

        state.equipState = PauseStateMenu.EquipmentState.NONE;
        DisableEquipmentButtons();
    }

    //Event Handler for button equipment list effects
    private void onSelectedButtonChange(object sender, EventArgs e)
    {
        clearSelectedEquipmentItem();
    }

    //button equipment list effects
    private void clearSelectedEquipmentItem() 
    {
        if (state.lastSelectedButton == null)
            return;

        UI_CharacterEquipmentSlot lastButton = state.lastSelectedButton.GetComponent<UI_CharacterEquipmentSlot>();
        lastButton.itemEquipmentName.color = unselectedTextColor;
        lastButton.barIcon.color = buttonUnchooseColorEquipment;
        lastButton.grButton.enabled = true;
    }

    //this will be for the next character button
    public void nextCharacterButton()
    {
        sfx.playSelectEquipmentButton();
        resetStateAndSelectedItem();
        GetComponent<EquipmentItemList>().RefreshInventoryItems();
        partyCount++;
        checkNextPreviousButtons();
        checkCharacter();
        equipMenu.refreshCharacterEquipmentList();
        equipMenu.resetCharacterStatsVisual();
    }

    //this will be for the previous character button
    public void previousCharacterButton()
    {
        sfx.playSelectEquipmentButton();
        resetStateAndSelectedItem();
        GetComponent<EquipmentItemList>().RefreshInventoryItems();
        partyCount--;
        checkNextPreviousButtons();
        checkCharacter();
        equipMenu.refreshCharacterEquipmentList();
        equipMenu.resetCharacterStatsVisual();
    }

    //this will be checker for character 
    //selected
    private void checkCharacter()
    {
        if(partyCount == 1)
        {
            movePosX.posX = mainCharCamPosX;
            PartyMembers.memType = partyMembers.memberType.mainCharacter;
            classIcon.sprite = PartyMembers.getIconSpriteCharacter();
            if(isBoy.getsetBoyGirlChecker)
            {
                className.text = "Wizard";
            }
            else
            {
                className.text = "Witch";
            }
            state.memType = "main character";
        }
        else if(partyCount == 2)
        {
            movePosX.posX = friendOneCamPosX;
            PartyMembers.memType = partyMembers.memberType.member1;
            classIcon.sprite = PartyMembers.getIconSpriteCharacter();
            className.text = "Magician";
            state.memType = "member1";
        }
        else if(partyCount == 3)
        {
            movePosX.posX = friendTwoCamPosX;
            PartyMembers.memType = partyMembers.memberType.member2;
            classIcon.sprite = PartyMembers.getIconSpriteCharacter();
            className.text = "Sorcerer";
            state.memType = "member2";
        }
        else if(partyCount == 4)
        {
            movePosX.posX = friendThreeCamPosX;
            PartyMembers.memType = partyMembers.memberType.member3;
            classIcon.sprite = PartyMembers.getIconSpriteCharacter();
            className.text = "Alchemist";
            state.memType = "member3";
        }
    }

    //camera characters equipment
    private void resetEquipmentCameraPosition()
    {
        if(partyCount == 1)
        {
            equipmentCamera.transform.position = new Vector3(mainCharCamPosX, equipmentCamera.transform.position.y, equipmentCamera.transform.position.z);
        }
        else if(partyCount == 2)
        {
            equipmentCamera.transform.position = new Vector3(friendOneCamPosX, equipmentCamera.transform.position.y, equipmentCamera.transform.position.z);
        }
        else if(partyCount == 3)
        {
            equipmentCamera.transform.position = new Vector3(friendTwoCamPosX, equipmentCamera.transform.position.y, equipmentCamera.transform.position.z);
        }
        else if(partyCount == 4)
        {
            equipmentCamera.transform.position = new Vector3(friendThreeCamPosX, equipmentCamera.transform.position.y, equipmentCamera.transform.position.z);
        }
    }

    //this will be for the checker if the
    //next button and previous button
    //will be disabled
    private void checkNextPreviousButtons()
    {
        if(party.GetPartyMembers().Count == 1)
        {
            nextCharacter.SetActive(false);
            previousCharacter.SetActive(false);
        }
        else if(party.GetPartyMembers().Count > 1)
        {
            if(partyCount == 1)
            {
                previousCharacter.SetActive(false);
                nextCharacter.SetActive(true);
            }
            else if(partyCount < party.GetPartyMembers().Count && partyCount != 1)
            {
                previousCharacter.SetActive(true);
                nextCharacter.SetActive(true);
            }
            else if(partyCount == party.GetPartyMembers().Count)
            {
                previousCharacter.SetActive(true);
                nextCharacter.SetActive(false);
            }
        }
    }

    public void selectOffense()
    {
        sfx.playSelectEquipmentButton();
        state.equipState = PauseStateMenu.EquipmentState.OFFENSE;
        offenseImage.color = buttonChooseColorEquipment;
        offenseText.color = equipmentTextChooseColor;
        equipmentWindow.SetActive(true);
        GetComponent<EquipmentItemList>().RefreshInventoryItems();
        DisableEquipmentButtons();
    }

    public void selectDefense()
    {
        sfx.playSelectEquipmentButton();
        state.equipState = PauseStateMenu.EquipmentState.DEFENSE;
        defenseImage.color = buttonChooseColorEquipment;
        defenseText.color = equipmentTextChooseColor;
        equipmentWindow.SetActive(true);
        GetComponent<EquipmentItemList>().RefreshInventoryItems();
        DisableEquipmentButtons();
    }

    public void selectAccessory()
    {
        sfx.playSelectEquipmentButton();
        state.equipState = PauseStateMenu.EquipmentState.ACCESSORY;
        accessoryImage.color = buttonChooseColorEquipment;
        accessoryText.color = equipmentTextChooseColor;
        equipmentWindow.SetActive(true);
        GetComponent<EquipmentItemList>().RefreshInventoryItems();
        DisableEquipmentButtons();
    }

    public void DisableEquipmentButtons()
    {
        if(state.equipState == PauseStateMenu.EquipmentState.NONE)
        {
            foreach(GraphicRaycaster gr in equipmentButtonGR)
            {
                gr.enabled = true;
            }
        }
        else
        {
            foreach(GraphicRaycaster gr in equipmentButtonGR)
            {
                gr.enabled = false;
            }
        }
    }

    private void resetStateAndSelectedItem()
    {
        if(state.itemStateButton != "")
        {
            state.itemStateButton = "";
            itemSelect.setItemSelected(null);
            descriptionWindow.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
