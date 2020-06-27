using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BattleEventButtons
{

    public enum ButtonBattleState
    {
        NONE,
        KNOWLEDGEEFFECT,
        ITEMS
    }

    private ButtonBattleState buttonBattleState;
    private bool isActionSelected;
    private GameObject selectedButton;
    private GameObject lastSelectedButton;
    private attackMagic attackSelected;
    private GameObject selectedEnemyButton;
    private GameObject lastSelectedEnemyButton;

    private event EventHandler buttonStateChange, SelectedButtonChange, SelectedEnemyButtonChange;

    public event EventHandler ButtonStateChange
    {
        add
        {
            if (buttonStateChange == null || !buttonStateChange.GetInvocationList().Contains(value))
                buttonStateChange += value;
        }
        remove
        {
            buttonStateChange -= value;
        }
    }
    public event EventHandler selectedButtonChange
    {
        add
        {
            if (SelectedButtonChange == null || !SelectedButtonChange.GetInvocationList().Contains(value))
                SelectedButtonChange += value;
        }
        remove
        {
            SelectedButtonChange -= value;
        }
    }
    public event EventHandler selectedEnemyButtonChange
    {
        add
        {
            if (SelectedEnemyButtonChange == null || !SelectedEnemyButtonChange.GetInvocationList().Contains(value))
                SelectedEnemyButtonChange += value;
        }
        remove
        {
            SelectedEnemyButtonChange -= value;
        }
    }

    public ButtonBattleState getsetButtonBattleState
    {
        get { return buttonBattleState; }
        set
        {
            buttonBattleState = value;
            buttonStateChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool getsetIsActionSelected
    {
        get { return isActionSelected; }
        set { isActionSelected = value; }
    }

    public GameObject selectButton
    {
        get { return selectedButton; }
        set
        {
            lastSelectedButton = selectedButton;
            selectedButton = value;
            SelectedButtonChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameObject getLastSelectedButton
    {
        get { return lastSelectedButton; }
    }

    public GameObject EnemySelectedButton
    {
        get { return selectedEnemyButton; }
        set
        {
            lastSelectedEnemyButton = selectedEnemyButton;
            selectedEnemyButton = value;
            SelectedEnemyButtonChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public GameObject getLastEnemySelectedButton
    {
        get { return lastSelectedEnemyButton; }
    }

    public attackMagic attack
    {
        get { return attackSelected; }
        set { attackSelected = value; }
    }

}
