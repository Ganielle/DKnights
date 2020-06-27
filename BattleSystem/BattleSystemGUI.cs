using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BattleSystemGUI : MonoBehaviour
{
    [Header("Items Button")]
    [SerializeField] private Vector3 itemsSelectedPos;
    [SerializeField] private Vector3 itemsUnselectedPos;

    [Header("Color")]
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color KEffectunselectedColor, itemButtonunselectedColor;
    [SerializeField] private Color unselectedMethodColor;

    [Header("Text Mesh Pro")]
    [SerializeField] private TextMeshProUGUI selectedMethodName;
    [SerializeField] private TextMeshProUGUI selectedMethodDescription;
    [SerializeField] private TextMeshProUGUI selectedMethodStatsDescription;

    [Header("Image")]
    [SerializeField] private Image descriptionIcon;

    [Header("GameObjects")]
    [SerializeField] private GameObject KEffect;
    [SerializeField] private GameObject itemButton,KEffectText,itemButtonText;
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private GameObject characterListPanel;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private GameObject selectEnemyPanel;

    [Header("Prefabs")]
    [SerializeField] private GameObject actionButtonPrefab;
    [SerializeField] private GameObject characterListPrefab, enemyBarPrefab;

    [Header("attack magic scriptable object")]
    [SerializeField] private List<attackMagic> mcAttackList;
    [SerializeField] private List<attackMagic> friendOneAttackList, friendTwoAttackList, friendThreeAttackList;

    TextMeshProUGUI KEffectTxt, itemButtonTxt;
    BattleEventButtons buttons;
    Inventory inventory;
    Image KEffectImage,itemButtonImage;
    Canvas KEffectCanvas,itemButtonCanvas;
    RectTransform itemsPos;
    GameObject itemList, characterList, attackList, enemyList;
    Party partyMember;
    PlayerStats playerStats;
    friend1Stats friendOneStats;
    friend2Stats friendTwoStats;
    friend3Stats friendThreeStats;
    BattleStates battleStates;
    actionSelected actionSelect;
    SoundManager sfx;

    Vector3[] battleGUIOldPos;

    private void OnEnable()
    {
        this.buttons.ButtonStateChange += onButtonStateChange;
        this.battleStates.battleStateChange += onStateBattleChange;
        this.buttons.selectedButtonChange += onSelectedButtonChange;
        this.actionSelect.battleItemSelectedChange += onMethodSelectedChange;
        this.buttons.selectedEnemyButtonChange += onSelectedEnemyChange;
    }

    private void OnDisable()
    {
        this.buttons.ButtonStateChange -= onButtonStateChange;
        this.battleStates.battleStateChange -= onStateBattleChange;
        this.buttons.selectedButtonChange -= onSelectedButtonChange;
        this.actionSelect.battleItemSelectedChange -= onMethodSelectedChange;
        this.buttons.selectedEnemyButtonChange -= onSelectedEnemyChange;
    }

    private void Awake()
    {
        this.buttons = GameManager.instance.battleButtons;
        this.inventory = GameManager.instance.inventory;
        this.partyMember = GameManager.instance.party;
        this.playerStats = GameManager.instance.playerStats;
        this.friendOneStats = GameManager.instance.friend1CharStats;
        this.friendTwoStats = GameManager.instance.friend2CharStats;
        this.friendThreeStats = GameManager.instance.friend3CharStats;
        this.battleStates = GameManager.instance.battleStates;
        this.actionSelect = GameManager.instance.actionSelect;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

        //canvas
        KEffectCanvas = KEffect.GetComponent<Canvas>();
        itemButtonCanvas = itemButton.GetComponent<Canvas>();

        //image 
        KEffectImage = KEffect.GetComponent<Image>();
        itemButtonImage = itemButton.GetComponent<Image>();

        //text
        KEffectTxt = KEffectText.GetComponent<TextMeshProUGUI>();
        itemButtonTxt = itemButtonText.GetComponent<TextMeshProUGUI>();

        //Rect Transform
        itemsPos = itemButtonTxt.GetComponent<RectTransform>();

        buttons.getsetButtonBattleState = BattleEventButtons.ButtonBattleState.KNOWLEDGEEFFECT;
    }

    #region EVENTS
    private void onStateBattleChange(object sender, EventArgs e)
    {

        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        refreshAttackList();
        inputCharacterList();

        if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.ITEMS)
        {
            refreshInventory();
        }
    }
    private void onMethodSelectedChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        showDescription();
    }
    private void onSelectedButtonChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        unselectLastMethodItem();
    }
    private void onButtonStateChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        actionSelect.setAttackMagic(null);
        actionSelect.setItem(null);

        knowledgeEffectSelected();
        itemSelected();
    }
    private void onSelectedEnemyChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        if (buttons.getLastEnemySelectedButton != null)
        {
            buttons.getLastEnemySelectedButton.GetComponent<UI_selectEnemy>().buttonBar.color = unselectedMethodColor;
        }

    }

    #endregion

    #region METHOD STATES

    private void knowledgeEffectSelected()
    {
        battleStates.getsetMethodState = BattleStates.MethodState.NONE;
        if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.KNOWLEDGEEFFECT)
        {
            refreshAttackList();

            KEffectImage.color = selectedColor;
            KEffectTxt.text = "Knowledge Effect";

            //sorting order
            KEffectCanvas.sortingOrder = 4;
            itemButtonCanvas.sortingOrder = 3;
        }
        else
        {
            KEffectImage.color = KEffectunselectedColor;
            KEffectTxt.text = "K. Effect";
        }
    }
    private void itemSelected()
    {
        battleStates.getsetMethodState = BattleStates.MethodState.NONE;

        if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.ITEMS)
        {
            refreshInventory();

            itemButtonImage.color = selectedColor;
            itemButtonTxt.fontSize = 30;

            itemsPos.anchoredPosition = itemsSelectedPos;

            itemButtonCanvas.sortingOrder = 4;
            KEffectCanvas.sortingOrder = 3;
        }
        else
        {
            itemsPos.anchoredPosition = itemsUnselectedPos;
            itemButtonTxt.fontSize = 25;
            itemButtonImage.color = itemButtonunselectedColor;
        }
    }

    #endregion

    #region METHOD ITEMS

    private void unselectLastMethodItem()
    {
        if (buttons.getLastSelectedButton == null)
            return;

        UI_BattleButton battleButton = buttons.getLastSelectedButton.GetComponent<UI_BattleButton>();
        battleButton.buttonBar.color = unselectedMethodColor;
    }

    private void unselectLastEnemyItem()
    {
        if (buttons.getLastEnemySelectedButton == null)
            return;

        UI_selectEnemy enemyButton = buttons.getLastEnemySelectedButton.GetComponent<UI_selectEnemy>();
        enemyButton.buttonBar.color = unselectedMethodColor;
    }
    private void inputCharacterList()
    {
        
        if(battleStates.getsetBattleState == BattleStates.BattleState.FIGHTSTART)
        {
            foreach (partyMembers party in partyMember.GetPartyMembers())
            {
                characterList = Instantiate(characterListPrefab, characterListPanel.transform);

                RectTransform characterMask = characterList.GetComponent<RectTransform>().Find("mask").GetComponent<RectTransform>();
                RectTransform HealthSliderPanel = characterList.GetComponent<RectTransform>().Find("Health").GetComponent<RectTransform>();
                RectTransform ManaSliderPanel = characterList.GetComponent<RectTransform>().Find("Mana").GetComponent<RectTransform>();

                Image characterImage = characterMask.GetComponent<RectTransform>().Find("characterImage").GetComponent<Image>();

                TextMeshProUGUI characterName = characterList.GetComponent<RectTransform>().Find("characterName").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI characterLevel = characterList.GetComponent<RectTransform>().Find("currentLevel").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI currentHealth = characterList.GetComponent<RectTransform>().Find("currentHealth").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI currentMana = characterList.GetComponent<RectTransform>().Find("currentMana").GetComponent<TextMeshProUGUI>();
                Slider sliderHealth = HealthSliderPanel.GetComponent<RectTransform>().Find("HealthSlider").GetComponent<Slider>();
                Slider sliderMana = ManaSliderPanel.GetComponent<RectTransform>().Find("ManaSlider").GetComponent<Slider>();

                if (party.memType == partyMembers.memberType.mainCharacter)
                {
                    characterImage.sprite = party.getCharacterSpriteBattle();
                    currentHealth.text = Convert.ToString(playerStats.getsetPlayerHP);
                    characterLevel.text = Convert.ToString(playerStats.getsetPlayerLvl);
                    currentMana.text = Convert.ToString(playerStats.getsetPlayerMP);
                    characterName.text = playerStats.getsetPlayerFName + " " + playerStats.getsetPlayerLName;
                    sliderHealth.value = (float)playerStats.getsetPlayerHP / playerStats.getsetPlayerMaxHP;
                    sliderMana.value = (float)playerStats.getsetPlayerMP / playerStats.getsetPlayerMaxMP;
                }
                else if (party.memType == partyMembers.memberType.member1)
                {

                }
                else if (party.memType == partyMembers.memberType.member2)
                {

                }
                else if (party.memType == partyMembers.memberType.member3)
                {

                }
            }
        }
    }
    private void showDescription()
    {
        if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.NONE || actionSelect.getItem() == null &&
            actionSelect.getMethod() == null)
        {
            descriptionPanel.SetActive(false);
        }

        if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.KNOWLEDGEEFFECT && actionSelect.getMethod() != null)
        {
            descriptionPanel.SetActive(true);
            selectedMethodName.text = actionSelect.getMethod().attackName;
            selectedMethodDescription.text = actionSelect.getMethod().description;
            descriptionIcon.sprite = actionSelect.getMethod().getMagicSprite();
            if (actionSelect.getMethod().attackType == attackMagic.AttackType.NORMAL)
            {
                selectedMethodStatsDescription.text = "Damage: " + actionSelect.getMethod().damage + " (+K.E Stats) \tMana Cost: "
                    + actionSelect.getMethod().manaCost;
            }
            else if (actionSelect.getMethod().attackType == attackMagic.AttackType.MAGIC)
            {
                selectedMethodStatsDescription.text = "Damage: " + actionSelect.getMethod().damage + " (+K.E & Intellience Stats) \tMana Cost: "
                    + actionSelect.getMethod().manaCost;
            }
            else if (actionSelect.getMethod().attackType == attackMagic.AttackType.GIVEHEALTH)
            {
                selectedMethodStatsDescription.text = "Health Recovery: " + actionSelect.getMethod().healthMana + "HP (+Intellience Stats) \tMana Cost: "
                    + actionSelect.getMethod().manaCost;
            }
            else if (actionSelect.getMethod().attackType == attackMagic.AttackType.GIVEMANA)
            {
                selectedMethodStatsDescription.text = "Mana Recovery: " + actionSelect.getMethod().healthMana + "MANA (+Intellience Stats) \tMana Cost: "
                    + actionSelect.getMethod().manaCost;
            }
        }
        else if (buttons.getsetButtonBattleState == BattleEventButtons.ButtonBattleState.ITEMS && actionSelect.getItem() != null)
        {
            descriptionPanel.SetActive(true);
            selectedMethodName.text = actionSelect.getItem().data.itemName;
            selectedMethodDescription.text = actionSelect.getItem().data.itemDescription;
            descriptionIcon.sprite = actionSelect.getItem().GetSprite();

            if (actionSelect.getItem().data.itemType == item.ItemType.healthPotion)
            {
                selectedMethodStatsDescription.text = "Health Recovery: " + actionSelect.getItem().data.healthRegen;
            }
            else if (actionSelect.getItem().data.itemType == item.ItemType.manaPotion)
            {
                selectedMethodStatsDescription.text = "Health Recovery: " + actionSelect.getItem().data.manaRegen;
            }
        }
        else
        {
            descriptionPanel.SetActive(false);
            selectedMethodName.text = "";
            selectedMethodDescription.text = "";
            descriptionIcon.sprite = null;
            selectedMethodStatsDescription.text = "";
        }
    }

    private void mcAttackMagicList()
    {
        foreach (attackMagic attack in mcAttackList)
        {
            attackList = Instantiate(actionButtonPrefab, actionPanel.transform);
            Image icon = attackList.GetComponent<RectTransform>().Find("Icon").GetComponent<Image>();
            TextMeshProUGUI attackName = attackList.GetComponent<RectTransform>().Find("actionName").GetComponent<TextMeshProUGUI>();

            icon.sprite = attack.getMagicSprite();
            attackName.text = attack.attackName;

            attackList.GetComponent<UI_BattleButton>().attack = attack;
        }
    }
    private void friendOneAttackMagicList()
    {
        foreach (attackMagic attack in friendOneAttackList)
        {
            attackList = Instantiate(actionButtonPrefab, actionPanel.transform);
            Image icon = attackList.GetComponent<RectTransform>().Find("Icon").GetComponent<Image>();
            TextMeshProUGUI attackName = attackList.GetComponent<RectTransform>().Find("actionName").GetComponent<TextMeshProUGUI>();

            icon.sprite = attack.getMagicSprite();
            attackName.text = attack.attackName;
        }
    }
    private void friendTwoAttackMagicList()
    {
        foreach (attackMagic attack in friendTwoAttackList)
        {
            attackList = Instantiate(actionButtonPrefab, actionPanel.transform);
            Image icon = attackList.GetComponent<RectTransform>().Find("Icon").GetComponent<Image>();
            TextMeshProUGUI attackName = attackList.GetComponent<RectTransform>().Find("actionName").GetComponent<TextMeshProUGUI>();

            icon.sprite = attack.getMagicSprite();
            attackName.text = attack.attackName;
        }
    }
    private void friendThreeAttackMagicList()
    {
        foreach (attackMagic attack in friendThreeAttackList)
        {
            attackList = Instantiate(actionButtonPrefab, actionPanel.transform);
            Image icon = attackList.GetComponent<RectTransform>().Find("Icon").GetComponent<Image>();
            TextMeshProUGUI attackName = attackList.GetComponent<RectTransform>().Find("actionName").GetComponent<TextMeshProUGUI>();

            icon.sprite = attack.getMagicSprite();
            attackName.text = attack.attackName;
        }
    }

    private void showEnemyList()
    {
        if (battleStates.getsetMethodState == BattleStates.MethodState.CHANT)
        {
            foreach (Transform child in selectEnemyPanel.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (GameObject gameobject in battleStates.getEnemyGameObjects())
            {
                enemyList = Instantiate(enemyBarPrefab, selectEnemyPanel.transform);
                TextMeshProUGUI enemyName = enemyList.GetComponent<RectTransform>().Find("enemyName").GetComponent<TextMeshProUGUI>();

                enemyName.text = gameobject.name;
                enemyList.GetComponent<UI_selectEnemy>().enemy = gameobject;
            }
        }
    }

    #endregion

    #region BUTTONS

    public void kEffectStatus()
    {
        buttons.getsetButtonBattleState = BattleEventButtons.ButtonBattleState.KNOWLEDGEEFFECT;
    }
    public void itemsStatus()
    {
        buttons.getsetButtonBattleState = BattleEventButtons.ButtonBattleState.ITEMS;
    }

    public void chant()
    {
        battleStates.getsetMethodState = BattleStates.MethodState.CHANT;
        sfx.playSelectPauseMenuType();
        showEnemyList();
    }

    public void backButton()
    {
        if (battleStates.getsetMethodState == BattleStates.MethodState.CHANT)
        {
            sfx.playClosePauseMenu();
            battleStates.getsetMethodState = BattleStates.MethodState.SELECT;
            GameObject.FindGameObjectWithTag("BattleSystemOneManager").GetComponent<BattleSystem>().reShowBattleGUI();
        }
        else if (battleStates.getsetMethodState == BattleStates.MethodState.SELECTENEMY)
        {
            sfx.playPauseTypeBackButton();
            battleStates.getsetMethodState = BattleStates.MethodState.CHANT;
            actionSelect.SelectedEnemy = null;
            buttons.EnemySelectedButton = null;
        }

    }

    #endregion

    #region REFRESHER
    private void refreshInventory()
    {
        foreach (Transform child in actionPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData itemData in inventory.GetItemConsumableList())
        {
            itemList = Instantiate(actionButtonPrefab, actionPanel.transform);
            Image icon = itemList.GetComponent<RectTransform>().Find("Icon").GetComponent<Image>();
            TextMeshProUGUI actionName = itemList.GetComponent<RectTransform>().Find("actionName").GetComponent<TextMeshProUGUI>();
            icon.sprite = itemData.GetSprite();
            if (itemData.itemAmount > 1)
            {
                actionName.text = itemData.data.itemName + " x " + itemData.itemAmount;
            }
            else
            {

                actionName.text = itemData.data.itemName;
            }

            itemList.GetComponent<UI_BattleButton>().item = itemData;
        }
    }

    private void refreshAttackList()
    {
        foreach (Transform child in actionPanel.transform)
        {
            Destroy(child.gameObject);
        }

        if (battleStates.getsetBattleState == BattleStates.BattleState.PLAYERTURN)
        {
            mcAttackMagicList();
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERONETURN)
        {
            friendOneAttackMagicList();
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERTWOTURN)
        {
            friendTwoAttackMagicList();
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERTHREETURN)
        {
            friendThreeAttackMagicList();
        }
    }

    #endregion
}
