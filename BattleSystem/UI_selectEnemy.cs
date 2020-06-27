using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_selectEnemy : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color selectedButtonColor;

    [Header("Image")]
    public Image buttonBar;

    [HideInInspector] public GameObject enemy;

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

        actionSelect.SelectedEnemy = null;

        battleStates.getsetMethodState = BattleStates.MethodState.SELECTENEMY;

        EventSystem.current.SetSelectedGameObject(gameObject);
        battleEventButtons.EnemySelectedButton = EventSystem.current.currentSelectedGameObject;

        if (battleEventButtons.EnemySelectedButton == EventSystem.current.currentSelectedGameObject)
        {
            buttonBar.color = selectedButtonColor;
        }

        enemy.GetComponent<moveYLoop>().playSelectIndicator();

        actionSelect.SelectedEnemy = enemy;
    }
}
