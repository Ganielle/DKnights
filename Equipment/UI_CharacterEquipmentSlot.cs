using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UI_CharacterEquipmentSlot : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color selectedTextColor;
    [SerializeField] private Color unSelectedTextColor ,selectedButton;

    [Header("Image")]
    public Image barIcon;

    [Header("TextMeshPro")]
    public TextMeshProUGUI itemEquipmentName;

    [Header("Graphic Raycaster")]
    public GraphicRaycaster grButton;

    [HideInInspector] public ItemData Item;
    SoundManager sfx;

    itemSelected itemSelect;
    PauseStateMenu state;

    private void Awake()
    {
        this.itemSelect = GameManager.instance.itemSelect;
        this.state = GameManager.instance.pauseState;
        state.itemStateButtonOnChanged += onItemStateButtonChange;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    private void onItemStateButtonChange(object sender, EventArgs e)
    {
        if (state.itemStateButton == "" && grButton != null)
            grButton.enabled = true;
    }

    public void selectItem()
    {
        grButton.enabled = false;
        itemSelect.setItemSelected(null);
        //sfx
        sfx.playSelectEquipmentButton();

        //button state
        state.itemStateButton = "item selected";

        //button effects
        EventSystem.current.SetSelectedGameObject(gameObject);
        state.selectedButton = EventSystem.current.currentSelectedGameObject;

        if(state.selectedButton == EventSystem.current.currentSelectedGameObject)
            barIcon.color = selectedButton;

        itemEquipmentName.color = selectedTextColor;

        //item selected 
        itemSelect.setItemSelected(Item);
    }

    public void onPressDownButton() 
    {
        itemEquipmentName.color = selectedTextColor;
    }

    public void onReleaseButton() 
    {
        itemEquipmentName.color = unSelectedTextColor;
    }
}
