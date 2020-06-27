using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PauseStateMenu
{
    public enum PauseMenuState
    {
        NONE,
        PAUSEMENU,
        STATUS,
        EQUIPMENT,
        ITEMS,
        SYSTEM
    }

    public enum EquipmentState
    {
        NONE,
        OFFENSE,
        DEFENSE,
        ACCESSORY
    }

    private PauseMenuState pauseMenuState;
    private EquipmentState equipmentState;
    private string memberType;
    private string itemState;
    private GameObject buttonObject;
    private GameObject lastButton;

    private event EventHandler pauseStateOnChanged;

    public event EventHandler pauseStateChange
    {
        add
        {
            if (pauseStateOnChanged == null || !pauseStateOnChanged.GetInvocationList().Contains(value))
                pauseStateOnChanged += value;
        }
        remove
        {
            pauseStateOnChanged -= value;
        }
    }

    public event EventHandler itemStateButtonOnChanged, selectedButtonOnChange;

    public PauseStateMenu() 
    {
        memberType = "";
        itemState = "";
        buttonObject = null;
        lastButton = null;
    }

    public PauseMenuState getsetPauseMenuState
    {
        get { return pauseMenuState; }
        set 
        { 
            pauseMenuState = value;
            pauseStateOnChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public EquipmentState equipState
    {
        get{ return equipmentState; }
        set{ equipmentState = value; }
    }

    public string memType
    {
        get{ return memberType; }
        set{ memberType = value; }
    }

    public string itemStateButton 
    {
        get { return itemState;  }
        set 
        { 
            itemState = value;
            itemStateButtonOnChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameObject selectedButton 
    {
        get { return buttonObject;  }
        set
        {
            lastButton = buttonObject;
            buttonObject = value;
            selectedButtonOnChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameObject lastSelectedButton 
    {
        get { return lastButton; }
    }
}
