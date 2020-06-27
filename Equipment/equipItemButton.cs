
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class equipItemButton : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] private Color disabledButton;
    [SerializeField] private Color enabledButton;
    [SerializeField] private Color statsIncrease, statsDecrease, statsDefault;

    [Header("Graphic Raycaster")]
    [SerializeField] private GraphicRaycaster equipButton;
    [SerializeField] private GraphicRaycaster unequipButton;

    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI charKE;
    [SerializeField] private TextMeshProUGUI charKL, charIntel;

    [Header("Image")]
    [SerializeField] private Image equipBG;
    [SerializeField] private Image unequipBG;
    [SerializeField] private Image KEStatsIndicator, KLStatsIndicator, IntelligenceStatsIndicator;

    [Header("Sprite")]
    [SerializeField] private Sprite increaseStats;
    [SerializeField] private Sprite decreaseStats;

    SoundManager sfx;
    itemSelected itemSelect;
    equipmentMC mcEquipment;
    PauseStateMenu state;
    PlayerStats playerStats;
    member1Equipment memOneEquipment;
    member2Equipment memTwoEquipment;
    member3Equipment memThreeEquipment;
    friend1Stats friendOneStats;
    friend2Stats friendTwoStats;
    friend3Stats friendThreeStats;

    equipmentMenu equipMenu;

    private void OnEnable()
    {
        itemSelect.selectedItem += onSelectedItem;
        mcEquipment.itemOnChange += mcItemOnChange;
        memOneEquipment.itemOnChange += memOneEquipmentOnChange;
        memTwoEquipment.itemOnChange += memTwoEquipmentOnChange;
        memThreeEquipment.itemOnChange += memThreeEquipmentOnChange;
        playerStats.OnStatsChanged += onPlayerStatsChange;
    }

    private void OnDisable()
    {
        itemSelect.selectedItem -= onSelectedItem;
        mcEquipment.itemOnChange -= mcItemOnChange;
        memOneEquipment.itemOnChange -= memOneEquipmentOnChange;
        memTwoEquipment.itemOnChange -= memTwoEquipmentOnChange;
        memThreeEquipment.itemOnChange -= memThreeEquipmentOnChange;
        playerStats.OnStatsChanged -= onPlayerStatsChange;
    }

    private void Awake()
    {
        this.itemSelect = GameManager.instance.itemSelect;
        this.mcEquipment = GameManager.instance.mcEquipment;
        this.state = GameManager.instance.pauseState;
        this.playerStats = GameManager.instance.playerStats;
        this.memOneEquipment = GameManager.instance.memOneEquipment;
        this.memTwoEquipment = GameManager.instance.memTwoEquipment;
        this.friendOneStats = GameManager.instance.friend1CharStats;
        this.friendTwoStats = GameManager.instance.friend2CharStats;
        this.friendThreeStats = GameManager.instance.friend3CharStats;
        this.memThreeEquipment = GameManager.instance.memThreeEquipment;
    }

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        equipMenu = GetComponent<equipmentMenu>();
    }

    private void onPlayerStatsChange(object sender, EventArgs e)
    {
        checkCharacterStats();
    }

    //EVENT LISTENERS
    private void memThreeEquipmentOnChange(object sender, EventArgs e)
    {
        checkButton();
        checkCharacterStats();
    }
    private void memTwoEquipmentOnChange(object sender, EventArgs e)
    {
        checkButton();
        checkCharacterStats();
    }
    private void memOneEquipmentOnChange(object sender, EventArgs e)
    {
        checkButton();
        checkCharacterStats();
    }
    private void mcItemOnChange(object sender, EventArgs e)
    {
        checkButton();
    }
    private void onSelectedItem(object sender, EventArgs e)
    {
        if (itemSelect.getItemSelected() == null)
            return;

        checkButton();
        checkCharacterStats();
    }


    //EQUIP AND UNEQUIP BUTTON
    private void checkButton()
    {
        if (itemSelect.getItemSelected().isEquip) 
        {
            equipButton.enabled = false;
            unequipButton.enabled = true;

            equipBG.color = disabledButton;
            unequipBG.color = enabledButton;
        }
        else 
        {
            equipButton.enabled = true;
            unequipButton.enabled = false;

            equipBG.color = enabledButton;
            unequipBG.color = disabledButton;
        }
    }
    public void equipItem()
    {
        sfx.playEquipButton();
        if (state.memType == "main character")
        {
            mcEquipItem();
        }
        else if (state.memType == "member1")
        {
            irahWitzEquipItem();
        }
        else if (state.memType == "member2")
        {
            yuukiBelleEquipItem();
        }
        else if (state.memType == "member3")
        {
            reeveOwunEquipItem();
        }
    }
    public void unequipItem() 
    {
        sfx.playUnequipButton();
        if (state.memType == "main character") 
        {
            mcUnEquipItem();
        }
        else if (state.memType == "member1")
        {
            irahWitzUnequipItem();
        }
        else if (state.memType == "member2")
        {
            yuukiBelleUnequipItem();
        }
        else if(state.memType == "member3")
        {
            reeveOwunUnequipItem();
        }
    }

    //CHARACTER STATUS
    private void checkCharacterStats()
    {
        if (state.memType == "main character")
        {
            if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
            {
                mainCharOffenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
            {
                mainCharDefenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
            {
                mainCharAccessoryCheckStats();
            }
        }
        else if (state.memType == "member1")
        {
            if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
            {
                friendOneOffenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
            {
                friendOneDefenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
            {
                friendOneAccessoryCheckStats();
            }
        }
        else if (state.memType == "member2")
        {
            if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
            {
                friendTwoOffenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
            {
                friendTwoDefenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
            {
                friendTwoAccessoryCheckStats();
            }
        }
        else if (state.memType == "member3")
        {
            if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
            {
                friendThreeOffenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
            {
                friendThreeDefenseCheckStats();
            }
            else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
            {
                friendThreeAccessoryCheckStats();
            }
        }
        else
        {
            return;
        }
    }
    //MAIN CHARACTER
    private void mainCharOffenseCheckStats()
    {
        int currentKE = playerStats.getsetKnowledgeEffect;
        int selectItemKE = itemSelect.getItemSelected().data.attack;

        if (itemSelect.getItemSelected() != null && mcEquipment.getWeaponItem() != null)
        {
            int currentItemKE = mcEquipment.getWeaponItem().data.attack;
            //this will check for Knowledge Effect
            if (selectItemKE > currentItemKE)
            {
                charKE.text = Convert.ToString((currentKE - currentItemKE) + selectItemKE);
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE <= currentItemKE)
            {
                charKE.text = Convert.ToString(currentKE - selectItemKE);
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
        }
        else if (itemSelect.getItemSelected() != null && mcEquipment.getWeaponItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKE != 0)
            {
                charKE.text = Convert.ToString(currentKE + selectItemKE);
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void mainCharDefenseCheckStats()
    {
        int currentKL = playerStats.getsetKnowledgeLevel;
        int selectItemKL = itemSelect.getItemSelected().data.defense;

        if (itemSelect.getItemSelected() != null && mcEquipment.getArmorItem() != null)
        {
            int currentItemKL = mcEquipment.getArmorItem().data.defense;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && mcEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void mainCharAccessoryCheckStats()
    {
        int currentKE = playerStats.getsetKnowledgeEffect;
        int currentKL = playerStats.getsetKnowledgeLevel;
        int currentIntel = playerStats.getsetIntelligence;
        int selectItemKE = itemSelect.getItemSelected().data.attack;
        int selectItemKL = itemSelect.getItemSelected().data.defense;
        int selectItemIntel = itemSelect.getItemSelected().data.intelligence;

        if (itemSelect.getItemSelected() != null && mcEquipment.getAccessoryItem() != null)
        {
            int currentItemKE = mcEquipment.getAccessoryItem().data.attack;
            int currentItemKL = mcEquipment.getAccessoryItem().data.defense;
            int currentItemIntel = mcEquipment.getAccessoryItem().data.intelligence;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);

            //knowledge effect
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }

            //knowledge level
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }

            //intelligence
            if (selectItemIntel > currentItemIntel)
            {
                charIntel.color = statsIncrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemIntel < currentItemIntel)
            {
                charIntel.color = statsDecrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charIntel.text = Convert.ToString(currentIntel);
                charIntel.color = statsDefault;
                IntelligenceStatsIndicator.gameObject.SetActive(false);
                IntelligenceStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && mcEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }

    //Irah Witz
    private void friendOneOffenseCheckStats()
    {
        int currentKE = friendOneStats.getsetKnowledgeEffect;
        int selectItemKE = itemSelect.getItemSelected().data.attack;

        if (itemSelect.getItemSelected() != null && memOneEquipment.getWeaponItem() != null)
        {
            int currentItemKE = memOneEquipment.getWeaponItem().data.attack;
            //this will check for Knowledge Effect

            charKE.text = Convert.ToString((currentKE - currentItemKE) + selectItemKE);
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memOneEquipment.getWeaponItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKE != 0)
            {
                charKE.text = Convert.ToString(currentKE + selectItemKE);
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendOneDefenseCheckStats()
    {
        int currentKL = friendOneStats.getsetKnowledgeLevel;
        int selectItemKL = itemSelect.getItemSelected().data.defense;

        if (itemSelect.getItemSelected() != null && memOneEquipment.getArmorItem() != null)
        {
            int currentItemKL = memOneEquipment.getArmorItem().data.defense;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memOneEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendOneAccessoryCheckStats()
    {
        int currentKE = friendOneStats.getsetKnowledgeEffect;
        int currentKL = friendOneStats.getsetKnowledgeLevel;
        int currentIntel = friendOneStats.getsetIntelligence;
        int selectItemKE = itemSelect.getItemSelected().data.attack;
        int selectItemKL = itemSelect.getItemSelected().data.defense;
        int selectItemIntel = itemSelect.getItemSelected().data.intelligence;

        if (itemSelect.getItemSelected() != null && memOneEquipment.getAccessoryItem() != null)
        {
            int currentItemKE = memOneEquipment.getAccessoryItem().data.attack;
            int currentItemKL = memOneEquipment.getAccessoryItem().data.defense;
            int currentItemIntel = memOneEquipment.getAccessoryItem().data.intelligence;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);

            //knowledge effect
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }

            //knowledge level
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }

            //intelligence
            if (selectItemIntel > currentItemIntel)
            {
                charIntel.color = statsIncrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemIntel < currentItemIntel)
            {
                charIntel.color = statsDecrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charIntel.text = Convert.ToString(currentIntel);
                charIntel.color = statsDefault;
                IntelligenceStatsIndicator.gameObject.SetActive(false);
                IntelligenceStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memOneEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }

    //Yuuki Belle
    private void friendTwoOffenseCheckStats()
    {
        int currentKE = friendTwoStats.getsetKnowledgeEffect;
        int selectItemKE = itemSelect.getItemSelected().data.attack;

        if (itemSelect.getItemSelected() != null && memTwoEquipment.getWeaponItem() != null)
        {
            int currentItemKE = memTwoEquipment.getWeaponItem().data.attack;
            //this will check for Knowledge Effect

            charKE.text = Convert.ToString((currentKE - currentItemKE) + selectItemKE);
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memTwoEquipment.getWeaponItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKE != 0)
            {
                charKE.text = Convert.ToString(currentKE + selectItemKE);
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendTwoDefenseCheckStats()
    {
        int currentKL = friendTwoStats.getsetKnowledgeLevel;
        int selectItemKL = itemSelect.getItemSelected().data.defense;

        if (itemSelect.getItemSelected() != null && memTwoEquipment.getArmorItem() != null)
        {
            int currentItemKL = memTwoEquipment.getArmorItem().data.defense;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memTwoEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendTwoAccessoryCheckStats()
    {
        int currentKE = friendTwoStats.getsetKnowledgeEffect;
        int currentKL = friendTwoStats.getsetKnowledgeLevel;
        int currentIntel = friendTwoStats.getsetIntelligence;
        int selectItemKE = itemSelect.getItemSelected().data.attack;
        int selectItemKL = itemSelect.getItemSelected().data.defense;
        int selectItemIntel = itemSelect.getItemSelected().data.intelligence;

        if (itemSelect.getItemSelected() != null && memTwoEquipment.getAccessoryItem() != null)
        {
            int currentItemKE = memTwoEquipment.getAccessoryItem().data.attack;
            int currentItemKL = memTwoEquipment.getAccessoryItem().data.defense;
            int currentItemIntel = memTwoEquipment.getAccessoryItem().data.intelligence;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);

            //knowledge effect
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }

            //knowledge level
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }

            //intelligence
            if (selectItemIntel > currentItemIntel)
            {
                charIntel.color = statsIncrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemIntel < currentItemIntel)
            {
                charIntel.color = statsDecrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charIntel.text = Convert.ToString(currentIntel);
                charIntel.color = statsDefault;
                IntelligenceStatsIndicator.gameObject.SetActive(false);
                IntelligenceStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memTwoEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }

    //REEVE OWUN
    private void friendThreeOffenseCheckStats()
    {
        int currentKE = friendThreeStats.getsetKnowledgeEffect;
        int selectItemKE = itemSelect.getItemSelected().data.attack;

        if (itemSelect.getItemSelected() != null && memThreeEquipment.getWeaponItem() != null)
        {
            int currentItemKE = memThreeEquipment.getWeaponItem().data.attack;
            //this will check for Knowledge Effect

            charKE.text = Convert.ToString((currentKE - currentItemKE) + selectItemKE);
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memThreeEquipment.getWeaponItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKE != 0)
            {
                charKE.text = Convert.ToString(currentKE + selectItemKE);
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendThreeDefenseCheckStats()
    {
        int currentKL = friendThreeStats.getsetKnowledgeLevel;
        int selectItemKL = itemSelect.getItemSelected().data.defense;

        if (itemSelect.getItemSelected() != null && memThreeEquipment.getArmorItem() != null)
        {
            int currentItemKL = memThreeEquipment.getArmorItem().data.defense;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memThreeEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }
    private void friendThreeAccessoryCheckStats()
    {
        int currentKE = friendThreeStats.getsetKnowledgeEffect;
        int currentKL = friendThreeStats.getsetKnowledgeLevel;
        int currentIntel = friendThreeStats.getsetIntelligence;
        int selectItemKE = itemSelect.getItemSelected().data.attack;
        int selectItemKL = itemSelect.getItemSelected().data.defense;
        int selectItemIntel = itemSelect.getItemSelected().data.intelligence;

        if (itemSelect.getItemSelected() != null && memThreeEquipment.getAccessoryItem() != null)
        {
            int currentItemKE = memThreeEquipment.getAccessoryItem().data.attack;
            int currentItemKL = memThreeEquipment.getAccessoryItem().data.defense;
            int currentItemIntel = memThreeEquipment.getAccessoryItem().data.intelligence;
            //this will check for Knowledge Effect

            charKL.text = Convert.ToString((currentKL - currentItemKL) + selectItemKL);

            //knowledge effect
            if (selectItemKE > currentItemKE)
            {
                charKE.color = statsIncrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKE < currentItemKE)
            {
                charKE.color = statsDecrease;
                KEStatsIndicator.gameObject.SetActive(true);
                KEStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKE.text = Convert.ToString(currentKE);
                charKE.color = statsDefault;
                KEStatsIndicator.gameObject.SetActive(false);
                KEStatsIndicator.sprite = null;
            }

            //knowledge level
            if (selectItemKL > currentItemKL)
            {
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemKL < currentItemKL)
            {
                charKL.color = statsDecrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charKL.text = Convert.ToString(currentKL);
                charKL.color = statsDefault;
                KLStatsIndicator.gameObject.SetActive(false);
                KLStatsIndicator.sprite = null;
            }

            //intelligence
            if (selectItemIntel > currentItemIntel)
            {
                charIntel.color = statsIncrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = increaseStats;
            }
            else if (selectItemIntel < currentItemIntel)
            {
                charIntel.color = statsDecrease;
                IntelligenceStatsIndicator.gameObject.SetActive(true);
                IntelligenceStatsIndicator.sprite = decreaseStats;
            }
            else
            {
                charIntel.text = Convert.ToString(currentIntel);
                charIntel.color = statsDefault;
                IntelligenceStatsIndicator.gameObject.SetActive(false);
                IntelligenceStatsIndicator.sprite = null;
            }
        }
        else if (itemSelect.getItemSelected() != null && memThreeEquipment.getArmorItem() == null)
        {

            //this will check for Knowledge Effect
            if (selectItemKL != 0)
            {
                charKL.text = Convert.ToString(currentKL + selectItemKL);
                charKL.color = statsIncrease;
                KLStatsIndicator.gameObject.SetActive(true);
                KLStatsIndicator.sprite = increaseStats;
            }
        }
    }


    //MAIN CHARACTER
    private void mcEquipItem() 
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (mcEquipment.getWeaponItem() != null)
            {
                playerStats.getsetKnowledgeEffect -= mcEquipment.getWeaponItem().data.attack;
                mcEquipment.getWeaponItem().isEquip = false;
                mcEquipment.getWeaponItem().itemEquipMemberType = "";
            }
            mcEquipment.setWeaponItem(itemSelect.getItemSelected());
            mcEquipment.getWeaponItem().isEquip = true;
            mcEquipment.getWeaponItem().itemEquipMemberType = playerStats.getsetPlayerFName + " " + playerStats.getsetPlayerLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if(mcEquipment.getArmorItem() != null) 
            {
                playerStats.getsetKnowledgeLevel -= mcEquipment.getArmorItem().data.defense;
                mcEquipment.getArmorItem().isEquip = false;
                mcEquipment.getArmorItem().itemEquipMemberType = "";
            }
            mcEquipment.setArmorItem(itemSelect.getItemSelected());
            mcEquipment.getArmorItem().isEquip = true;
            mcEquipment.getArmorItem().itemEquipMemberType = playerStats.getsetPlayerFName + " " + playerStats.getsetPlayerLName; 
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if(mcEquipment.getAccessoryItem() != null) 
            {
                playerStats.getsetKnowledgeEffect -= mcEquipment.getAccessoryItem().data.attack;
                playerStats.getsetKnowledgeLevel -= mcEquipment.getAccessoryItem().data.defense;
                playerStats.getsetIntelligence -= mcEquipment.getAccessoryItem().data.intelligence;
                mcEquipment.getAccessoryItem().isEquip = false;
                mcEquipment.getAccessoryItem().itemEquipMemberType = "";
            }
            mcEquipment.setAccessoryItem(itemSelect.getItemSelected());
            mcEquipment.getAccessoryItem().isEquip = true;
            mcEquipment.getAccessoryItem().itemEquipMemberType = playerStats.getsetPlayerFName + " " + playerStats.getsetPlayerLName;
        }
        checkButton();
        updateEquipMainCharStats();
    }
    private void mcUnEquipItem() 
    {
        updateUnequipMainCharStats();
        if(state.equipState == PauseStateMenu.EquipmentState.OFFENSE) 
        {
            if(mcEquipment.getWeaponItem() != null) 
            {
                mcEquipment.getWeaponItem().isEquip = false;
                mcEquipment.getWeaponItem().itemEquipMemberType = "";
                mcEquipment.setWeaponItem(null);
            }
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.DEFENSE) 
        {
            if(mcEquipment.getArmorItem() != null) 
            {
                mcEquipment.getArmorItem().isEquip = false;
                mcEquipment.getArmorItem().itemEquipMemberType = "";
                mcEquipment.setArmorItem(null);
            }
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.ACCESSORY) 
        {
            if(mcEquipment.getAccessoryItem() != null) 
            {
                mcEquipment.getAccessoryItem().isEquip = false;
                mcEquipment.getAccessoryItem().itemEquipMemberType = "";
                mcEquipment.setAccessoryItem(null);
            }
        }
    }
    private void updateEquipMainCharStats() 
    {
        if(state.equipState == PauseStateMenu.EquipmentState.OFFENSE) 
        {
            playerStats.getsetKnowledgeEffect += mcEquipment.getWeaponItem().data.attack;
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.DEFENSE) 
        {
            playerStats.getsetKnowledgeLevel += mcEquipment.getArmorItem().data.defense;
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.ACCESSORY) 
        {
            playerStats.getsetKnowledgeEffect += mcEquipment.getAccessoryItem().data.attack;
            playerStats.getsetKnowledgeLevel += mcEquipment.getAccessoryItem().data.defense;
            playerStats.getsetIntelligence += mcEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }
    private void updateUnequipMainCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            playerStats.getsetKnowledgeEffect -= mcEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            playerStats.getsetKnowledgeLevel -= mcEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            playerStats.getsetKnowledgeEffect -= mcEquipment.getAccessoryItem().data.attack;
            playerStats.getsetKnowledgeLevel -= mcEquipment.getAccessoryItem().data.defense;
            playerStats.getsetIntelligence -= mcEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }


    //MEMBER 1
    private void irahWitzEquipItem() 
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memOneEquipment.getWeaponItem() != null)
            {
                friendOneStats.getsetKnowledgeEffect -= memOneEquipment.getWeaponItem().data.attack;
                memOneEquipment.getWeaponItem().isEquip = false;
                memOneEquipment.getWeaponItem().itemEquipMemberType = "";
            }
            memOneEquipment.setWeaponItem(itemSelect.getItemSelected());
            memOneEquipment.getWeaponItem().isEquip = true;
            memOneEquipment.getWeaponItem().itemEquipMemberType = friendOneStats.getCharFname + " " + friendOneStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memOneEquipment.getArmorItem() != null)
            {
                friendOneStats.getsetKnowledgeLevel -= memOneEquipment.getArmorItem().data.defense;
                memOneEquipment.getArmorItem().isEquip = false;
                memOneEquipment.getArmorItem().itemEquipMemberType = "";
            }
            memOneEquipment.setArmorItem(itemSelect.getItemSelected());
            memOneEquipment.getArmorItem().isEquip = true;
            memOneEquipment.getArmorItem().itemEquipMemberType = friendOneStats.getCharFname + " " + friendOneStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memOneEquipment.getAccessoryItem() != null)
            {
                friendOneStats.getsetKnowledgeEffect -= memOneEquipment.getAccessoryItem().data.attack;
                friendOneStats.getsetKnowledgeLevel -= memOneEquipment.getAccessoryItem().data.defense;
                friendOneStats.getsetIntelligence -= memOneEquipment.getAccessoryItem().data.intelligence;
                memOneEquipment.getAccessoryItem().isEquip = false;
                memOneEquipment.getAccessoryItem().itemEquipMemberType = "";
            }
            memOneEquipment.setAccessoryItem(itemSelect.getItemSelected());
            memOneEquipment.getAccessoryItem().isEquip = true;
            memOneEquipment.getAccessoryItem().itemEquipMemberType = friendOneStats.getCharFname + " " + friendOneStats.getCharLName;
        }
        checkButton();
        updateEquipIrahWitzCharStats();
    }
    private void irahWitzUnequipItem()
    {
        updateUnequipIrahWitzCharStats();
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memOneEquipment.getWeaponItem() != null)
            {
                memOneEquipment.getWeaponItem().isEquip = false;
                memOneEquipment.getWeaponItem().itemEquipMemberType = "";
                memOneEquipment.setWeaponItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memOneEquipment.getArmorItem() != null)
            {
                memOneEquipment.getArmorItem().isEquip = false;
                memOneEquipment.getArmorItem().itemEquipMemberType = "";
                memOneEquipment.setArmorItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memOneEquipment.getAccessoryItem() != null)
            {
                memOneEquipment.getAccessoryItem().isEquip = false;
                memOneEquipment.getAccessoryItem().itemEquipMemberType = "";
                memOneEquipment.setAccessoryItem(null);
            }
        }
    }
    private void updateEquipIrahWitzCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendOneStats.getsetKnowledgeEffect += memOneEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendOneStats.getsetKnowledgeLevel += memOneEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendOneStats.getsetKnowledgeEffect += memOneEquipment.getAccessoryItem().data.attack;
            friendOneStats.getsetKnowledgeLevel += memOneEquipment.getAccessoryItem().data.defense;
            friendOneStats.getsetIntelligence += memOneEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }
    private void updateUnequipIrahWitzCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendOneStats.getsetKnowledgeEffect -= memOneEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendOneStats.getsetKnowledgeLevel -= memOneEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendOneStats.getsetKnowledgeEffect -= memOneEquipment.getAccessoryItem().data.attack;
            friendOneStats.getsetKnowledgeLevel -= memOneEquipment.getAccessoryItem().data.defense;
            friendOneStats.getsetIntelligence -= memOneEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }

    //MEMBER 2
    private void yuukiBelleEquipItem()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memTwoEquipment.getWeaponItem() != null)
            {
                friendTwoStats.getsetKnowledgeEffect -= memTwoEquipment.getWeaponItem().data.attack;
                memTwoEquipment.getWeaponItem().isEquip = false;
                memTwoEquipment.getWeaponItem().itemEquipMemberType = "";
            }
            memTwoEquipment.setWeaponItem(itemSelect.getItemSelected());
            memTwoEquipment.getWeaponItem().isEquip = true;
            memTwoEquipment.getWeaponItem().itemEquipMemberType = friendTwoStats.getCharFname + " " + friendTwoStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memTwoEquipment.getArmorItem() != null)
            {
                friendTwoStats.getsetKnowledgeLevel -= memTwoEquipment.getArmorItem().data.defense;
                memTwoEquipment.getArmorItem().isEquip = false;
                memTwoEquipment.getArmorItem().itemEquipMemberType = "";
            }
            memTwoEquipment.setArmorItem(itemSelect.getItemSelected());
            memTwoEquipment.getArmorItem().isEquip = true;
            memTwoEquipment.getArmorItem().itemEquipMemberType = friendTwoStats.getCharFname + " " + friendTwoStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memTwoEquipment.getAccessoryItem() != null)
            {
                friendTwoStats.getsetKnowledgeEffect -= memTwoEquipment.getAccessoryItem().data.attack;
                friendTwoStats.getsetKnowledgeLevel -= memTwoEquipment.getAccessoryItem().data.defense;
                friendTwoStats.getsetIntelligence -= memTwoEquipment.getAccessoryItem().data.intelligence;
                memTwoEquipment.getAccessoryItem().isEquip = false;
                memTwoEquipment.getAccessoryItem().itemEquipMemberType = "";
            }
            memTwoEquipment.setAccessoryItem(itemSelect.getItemSelected());
            memTwoEquipment.getAccessoryItem().isEquip = true;
            memTwoEquipment.getAccessoryItem().itemEquipMemberType = friendTwoStats.getCharFname + " " + friendTwoStats.getCharLName;
        }
        checkButton();
        updateEquipYuukiBelleCharStats();
    }
    private void yuukiBelleUnequipItem()
    {
        updateUnequipYuukiBelleCharStats();
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memTwoEquipment.getWeaponItem() != null)
            {
                memTwoEquipment.getWeaponItem().isEquip = false;
                memTwoEquipment.getWeaponItem().itemEquipMemberType = "";
                memTwoEquipment.setWeaponItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memTwoEquipment.getArmorItem() != null)
            {
                memTwoEquipment.getArmorItem().isEquip = false;
                memTwoEquipment.getArmorItem().itemEquipMemberType = "";
                memTwoEquipment.setArmorItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memTwoEquipment.getAccessoryItem() != null)
            {
                memTwoEquipment.getAccessoryItem().isEquip = false;
                memTwoEquipment.getAccessoryItem().itemEquipMemberType = "";
                memTwoEquipment.setAccessoryItem(null);
            }
        }
    }
    private void updateEquipYuukiBelleCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendTwoStats.getsetKnowledgeEffect += memTwoEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendTwoStats.getsetKnowledgeLevel += memTwoEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendTwoStats.getsetKnowledgeEffect += memTwoEquipment.getAccessoryItem().data.attack;
            friendTwoStats.getsetKnowledgeLevel += memTwoEquipment.getAccessoryItem().data.defense;
            friendTwoStats.getsetIntelligence += memTwoEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }
    private void updateUnequipYuukiBelleCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendTwoStats.getsetKnowledgeEffect -= memTwoEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendTwoStats.getsetKnowledgeLevel -= memTwoEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendTwoStats.getsetKnowledgeEffect -= memTwoEquipment.getAccessoryItem().data.attack;
            friendTwoStats.getsetKnowledgeLevel -= memTwoEquipment.getAccessoryItem().data.defense;
            friendTwoStats.getsetIntelligence -= memTwoEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }

    //MEMBER 3
    private void reeveOwunEquipItem()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memThreeEquipment.getWeaponItem() != null)
            {
                friendThreeStats.getsetKnowledgeEffect -= memThreeEquipment.getWeaponItem().data.attack;
                memThreeEquipment.getWeaponItem().isEquip = false;
                memThreeEquipment.getWeaponItem().itemEquipMemberType = "";
            }
            memThreeEquipment.setWeaponItem(itemSelect.getItemSelected());
            memThreeEquipment.getWeaponItem().isEquip = true;
            memThreeEquipment.getWeaponItem().itemEquipMemberType = friendThreeStats.getCharFname + " " + friendThreeStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memThreeEquipment.getArmorItem() != null)
            {
                friendThreeStats.getsetKnowledgeLevel -= memThreeEquipment.getArmorItem().data.defense;
                memThreeEquipment.getArmorItem().isEquip = false;
                memThreeEquipment.getArmorItem().itemEquipMemberType = "";
            }
            memThreeEquipment.setArmorItem(itemSelect.getItemSelected());
            memThreeEquipment.getArmorItem().isEquip = true;
            memThreeEquipment.getArmorItem().itemEquipMemberType = friendThreeStats.getCharFname + " " + friendThreeStats.getCharLName;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memThreeEquipment.getAccessoryItem() != null)
            {
                friendThreeStats.getsetKnowledgeEffect -= memThreeEquipment.getAccessoryItem().data.attack;
                friendThreeStats.getsetKnowledgeLevel -= memThreeEquipment.getAccessoryItem().data.defense;
                friendThreeStats.getsetIntelligence -= memThreeEquipment.getAccessoryItem().data.intelligence;
                memThreeEquipment.getAccessoryItem().isEquip = false;
                memThreeEquipment.getAccessoryItem().itemEquipMemberType = "";
            }
            memThreeEquipment.setAccessoryItem(itemSelect.getItemSelected());
            memThreeEquipment.getAccessoryItem().isEquip = true;
            memOneEquipment.getAccessoryItem().itemEquipMemberType = friendThreeStats.getCharFname + " " + friendThreeStats.getCharLName;
        }
        checkButton();
        updateEquipReeveOwunCharStats();
    }
    private void reeveOwunUnequipItem()
    {
        updateUnequipReeveOwunCharStats();
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if (memThreeEquipment.getWeaponItem() != null)
            {
                memThreeEquipment.getWeaponItem().isEquip = false;
                memThreeEquipment.getWeaponItem().itemEquipMemberType = "";
                memThreeEquipment.setWeaponItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            if (memThreeEquipment.getArmorItem() != null)
            {
                memThreeEquipment.getArmorItem().isEquip = false;
                memThreeEquipment.getArmorItem().itemEquipMemberType = "";
                memThreeEquipment.setArmorItem(null);
            }
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            if (memThreeEquipment.getAccessoryItem() != null)
            {
                memThreeEquipment.getAccessoryItem().isEquip = false;
                memThreeEquipment.getAccessoryItem().itemEquipMemberType = "";
                memThreeEquipment.setAccessoryItem(null);
            }
        }
    }
    private void updateEquipReeveOwunCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendThreeStats.getsetKnowledgeEffect += memThreeEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendThreeStats.getsetKnowledgeLevel += memThreeEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendThreeStats.getsetKnowledgeEffect += memThreeEquipment.getAccessoryItem().data.attack;
            friendThreeStats.getsetKnowledgeLevel += memThreeEquipment.getAccessoryItem().data.defense;
            friendThreeStats.getsetIntelligence += memThreeEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }
    private void updateUnequipReeveOwunCharStats()
    {
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            friendThreeStats.getsetKnowledgeEffect -= memThreeEquipment.getWeaponItem().data.attack;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            friendThreeStats.getsetKnowledgeLevel -= memThreeEquipment.getArmorItem().data.defense;
        }
        else if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            friendThreeStats.getsetKnowledgeEffect -= memThreeEquipment.getAccessoryItem().data.attack;
            friendThreeStats.getsetKnowledgeLevel -= memThreeEquipment.getAccessoryItem().data.defense;
            friendThreeStats.getsetIntelligence -= memThreeEquipment.getAccessoryItem().data.intelligence;
        }

        equipMenu.resetCharacterStatsVisual();
    }
}
