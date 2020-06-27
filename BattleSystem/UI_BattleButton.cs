using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_BattleButton : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color selectedButtonColor;

    [Header("Image")]
    public Image buttonBar;

    [HideInInspector] public attackMagic attack;
    [HideInInspector] public ItemData item;

    SoundManager sfx;
    BattleEventButtons battleEventButtons;
    actionSelected actionSelect;
    BattleStates battleStates;

    private void Awake()
    {
        this.battleStates = GameManager.instance.battleStates;
        this.battleEventButtons = GameManager.instance.battleButtons;
        this.actionSelect = GameManager.instance.actionSelect;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    public void selectMethod()
    {
        sfx.playSelectEquipmentButton();

        actionSelect.setItem(null);
        actionSelect.setAttackMagic(null);

        battleStates.getsetMethodState = BattleStates.MethodState.SELECT;

        EventSystem.current.SetSelectedGameObject(gameObject);
        battleEventButtons.selectButton = EventSystem.current.currentSelectedGameObject;

        if (battleEventButtons.selectButton == EventSystem.current.currentSelectedGameObject)
        {
            buttonBar.color = selectedButtonColor;
        }

        if(battleEventButtons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.KNOWLEDGEEFFECT)
        {
            actionSelect.setAttackMagic(attack);
        }
        else if(battleEventButtons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.ITEMS)
        {
            actionSelect.setItem(item);
        }
        else
        {
            return;
        }
    }
}
