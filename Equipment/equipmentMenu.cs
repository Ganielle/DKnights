using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class equipmentMenu : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField]private Color equipmentButtonChooseColor;
    [SerializeField]private Color equipmentButtonUnchooseColor;
    [SerializeField] private Color statsIncrease, statsDecrease, statsDefault;

    [Header("Text Mesh Pro")]
    [SerializeField]private TextMeshProUGUI equipmentDesc;
    [SerializeField]private TextMeshProUGUI charName, charMaxExp, charExp, charHp, charMp, charLvl;
    [SerializeField]private TextMeshProUGUI charKE, charKL, charIntel;
    [SerializeField] private TextMeshProUGUI charOffenseEquipmentName, charDefenseEquipmentName, charAccessoryEquipmentName;

    [Header("Description UI")]
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemDescriptionName, itemStats;

    [Header("Slider")]
    [SerializeField]private Slider charExpNormalized;
    [SerializeField]private Slider charHpNormalized, charMpNormalized;

    [Header ("Descriptions")]
    [TextArea]
    [SerializeField]private string offenseDesc;
    [TextArea]
    [SerializeField]private string defenseDesc, accessoryDesc;

    [Header("Images")]
    [SerializeField] private Image offenseEquipmentImage;
    [SerializeField] private Image defenseEquipmentImage;
    [SerializeField] private Image accessoryEquipmentImage;
    [SerializeField] private Image KEStatsIndicator, KLStatsIndicator, IntelligenceStatsIndicator;
    [SerializeField] private Image offenseRarity, defenseRarity, accessoryRarity;

    [Header("Description UI")]
    [SerializeField] private Image itemDescriptionImage;
    [SerializeField] private Image charOffenseSprite, charDefenseSprite, charAccessorySprite;

    [Header("Game Objects")]
    [SerializeField] private GameObject descriptionWindow;

    itemSelected itemSelect;
    PauseStateMenu state;
    PlayerStats playerStats;
    friend1Stats friend1;
    friend2Stats friend2;
    friend3Stats friend3;
    equipmentMC mcEquipment;
    member1Equipment memOneEquipment;
    member2Equipment memTwoEquipment;
    member3Equipment memThreeEquipment;
    MovingRectTransform descriptionLeanTween;

    private void OnEnable()
    {
        itemSelect.selectedItem += onSelectedItemChange;
        state.itemStateButtonOnChanged += onItemStateButtonChange;
        mcEquipment.itemOnChange += mcEquipmentOnChange;
        memOneEquipment.itemOnChange += memOneEquipmentOnChange;
        memTwoEquipment.itemOnChange += memTwoEquipmentOnChange;
        memThreeEquipment.itemOnChange += memThreeEquipmentOnChange;
    }

    private void OnDisable()
    {
        itemSelect.selectedItem -= onSelectedItemChange;
        state.itemStateButtonOnChanged -= onItemStateButtonChange;
        mcEquipment.itemOnChange -= mcEquipmentOnChange;
        memOneEquipment.itemOnChange -= memOneEquipmentOnChange;
        memTwoEquipment.itemOnChange -= memTwoEquipmentOnChange;
        memThreeEquipment.itemOnChange -= memThreeEquipmentOnChange;
    }

    private void Awake()
    {
        this.state = GameManager.instance.pauseState;
        this.playerStats = GameManager.instance.playerStats;
        this.friend1 = GameManager.instance.friend1CharStats;
        this.friend2 = GameManager.instance.friend2CharStats;
        this.friend3 = GameManager.instance.friend3CharStats;
        this.itemSelect = GameManager.instance.itemSelect;
        this.mcEquipment = GameManager.instance.mcEquipment;
        this.memOneEquipment = GameManager.instance.memOneEquipment;
        this.memTwoEquipment = GameManager.instance.memTwoEquipment;
        this.memThreeEquipment = GameManager.instance.memThreeEquipment;
    }

    void Start()
    {
        state.memType = "main character";
        descriptionLeanTween = descriptionWindow.GetComponent<MovingRectTransform>();
        characterStatus();
        refreshCharacterEquipmentList();

        if(state.itemStateButton != "item selected") 
        {
            descriptionWindow.SetActive(false);
        }
    }

    //EVENT LISTENERS
    private void memThreeEquipmentOnChange(object sender, EventArgs e)
    {
        refreshCharacterEquipmentList();
    }

    private void memTwoEquipmentOnChange(object sender, EventArgs e)
    {
        refreshCharacterEquipmentList();
    }

    private void memOneEquipmentOnChange(object sender, EventArgs e)
    {
        refreshCharacterEquipmentList();
    }

    private void mcEquipmentOnChange(object sender, EventArgs e)
    {
        refreshCharacterEquipmentList();
    }


    //ITEM STATE BUTTONS
    private void onItemStateButtonChange(object sender, EventArgs e)
    {
        showDescriptionWindow();
    }

    private void onSelectedItemChange(object sender, EventArgs e)
    {
        if (itemSelect.getItemSelected() == null)
        {
            resetCharacterStatsVisual();
        }
        else
        {
            equipmenDescription();
        }

    }


    //EQUIPMENT UI
    public void equipmentUI()
    {
        //equipment select
        equipmentDesc.text = "";

        //offense
        if (state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            offenseEquipmentImage.color = equipmentButtonChooseColor;
            equipmentDesc.text = offenseDesc;
        }
        else
        {
            offenseEquipmentImage.color = equipmentButtonUnchooseColor;
        }

        //defense
        if (state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            defenseEquipmentImage.color = equipmentButtonChooseColor;
            equipmentDesc.text = defenseDesc;
        }
        else
        {
            defenseEquipmentImage.color = equipmentButtonUnchooseColor;
        }

        //accessory
        if (state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            accessoryEquipmentImage.color = equipmentButtonChooseColor;
            equipmentDesc.text = accessoryDesc;
        }
        else
        {
            accessoryEquipmentImage.color = equipmentButtonUnchooseColor;
        }

    }

    private void equipmenDescription() 
    {
        itemDescriptionImage.sprite = itemSelect.getItemSelected().GetSprite();
        itemDescriptionName.text = itemSelect.getItemSelected().data.itemName;
        itemDescription.text = itemSelect.getItemSelected().data.itemDescription;
        itemStats.text = "K.E.: " + itemSelect.getItemSelected().data.attack + "\t K.L.: " + itemSelect.getItemSelected().data.defense + "\t Intelligence: " + 
            itemSelect.getItemSelected().data.intelligence;
    }

    private void showDescriptionWindow() 
    {
        if(state.itemStateButton == "item selected") 
        {
            descriptionWindow.SetActive(true);
            descriptionLeanTween.moveRectTransform();
        }
    }


    //PARTY MEMBERS STATUS
    public void characterStatus()
    {
        if(state.memType == "main character")
        {
            mainCharStats();
        }
        else if(state.memType == "member1")
        {
            friend1Stats();
        }
        else if(state.memType == "member2")
        {
            friend2Stats();
        }
        else if(state.memType == "member3")
        {
            friend3Stats();
        }
    }

    private void mainCharStats()
    {
        //text mesh pro 
        charName.text = playerStats.getsetPlayerFName + " " + playerStats.getsetPlayerLName;
        charMaxExp.text = playerStats.getsetXPToNextLevelup.ToString();
        charExp.text = playerStats.getsetPlayerXP.ToString();
        charHp.text = playerStats.getsetPlayerHP.ToString();
        charMp.text = playerStats.getsetPlayerMP.ToString();
        charKE.text = playerStats.getsetKnowledgeEffect.ToString();
        charKL.text = playerStats.getsetKnowledgeLevel.ToString();
        charIntel.text = playerStats.getsetIntelligence.ToString();
        charLvl.text = playerStats.getsetPlayerLvl.ToString();

        //slider
        charExpNormalized.value = playerStats.getXPNormalized;
        charHpNormalized.value = playerStats.getPlayerHPNormalized;
        charMpNormalized.value = playerStats.getPlayerMPNormalized;
    }

    private void friend1Stats()
    {
        //text mesh pro 
        charName.text = friend1.getCharFname + " " + friend1.getCharLName;
        charMaxExp.text = friend1.getsetExpToNextLevel.ToString();
        charExp.text = friend1.getsetExp.ToString();
        charHp.text = friend1.getsetHp.ToString();
        charMp.text = friend1.getsetMp.ToString();
        charKE.text = friend1.getsetKnowledgeEffect.ToString();
        charKL.text = friend1.getsetKnowledgeLevel.ToString();
        charIntel.text = friend1.getsetIntelligence.ToString();
        charLvl.text = friend1.getsetLvl.ToString();

        //slider
        charExpNormalized.value = friend1.getExpNormalized;
        charHpNormalized.value = friend1.getHpNormalized;
        charMpNormalized.value = friend1.getMpNormalized;
    }

    private  void friend2Stats()
    {
        //text mesh pro 
        charName.text = friend2.getCharFname + " " + friend2.getCharLName;
        charMaxExp.text = friend2.getsetExpToNextLevel.ToString();
        charExp.text = friend2.getsetExp.ToString();
        charHp.text = friend2.getsetHp.ToString();
        charMp.text = friend2.getsetMp.ToString();
        charKE.text = friend2.getsetKnowledgeEffect.ToString();
        charKL.text = friend2.getsetKnowledgeLevel.ToString();
        charIntel.text = friend2.getsetIntelligence.ToString();
        charLvl.text = friend2.getsetLvl.ToString();

        //slider
        charExpNormalized.value = friend2.getExpNormalized;
        charHpNormalized.value = friend2.getHpNormalized;
        charMpNormalized.value = friend2.getMpNormalized;
    }

    private void friend3Stats()
    {
        //text mesh pro 
        charName.text = friend3.getCharFname + " " + friend3.getCharLName;
        charMaxExp.text = friend3.getsetExpToNextLevel.ToString();
        charExp.text = friend3.getsetExp.ToString();
        charHp.text = friend3.getsetHp.ToString();
        charMp.text = friend3.getsetMp.ToString();
        charKE.text = friend3.getsetKnowledgeEffect.ToString();
        charKL.text = friend3.getsetKnowledgeLevel.ToString();
        charIntel.text = friend3.getsetIntelligence.ToString();
        charLvl.text = friend3.getsetLvl.ToString();

        //slider
        charExpNormalized.value = friend3.getExpNormalized;
        charHpNormalized.value = friend3.getHpNormalized;
        charMpNormalized.value = friend3.getMpNormalized;
    }

    public void resetCharacterStatsVisual() 
    {
        if (state.itemStateButton != "")
            return;

        KEStatsIndicator.sprite = null;
        KEStatsIndicator.gameObject.SetActive(false);

        KLStatsIndicator.sprite = null;
        KLStatsIndicator.gameObject.SetActive(false);


        IntelligenceStatsIndicator.sprite = null;
        IntelligenceStatsIndicator.gameObject.SetActive(false);

        charKE.color = statsDefault;
        charKL.color = statsDefault;
        charIntel.color = statsDefault;

        if (state.memType == "main character")
        {
            charKE.text = Convert.ToString(playerStats.getsetKnowledgeEffect);
            charKL.text = Convert.ToString(playerStats.getsetKnowledgeLevel);
            charIntel.text = Convert.ToString(playerStats.getsetIntelligence);
        }
        else if (state.memType == "member1")
        {
            charKE.text = Convert.ToString(friend1.getsetKnowledgeEffect);
            charKL.text = Convert.ToString(friend1.getsetKnowledgeLevel);
            charIntel.text = Convert.ToString(friend1.getsetIntelligence);
        }
        else if (state.memType == "member2")
        {
            charKE.text = Convert.ToString(friend2.getsetKnowledgeEffect);
            charKL.text = Convert.ToString(friend2.getsetKnowledgeLevel);
            charIntel.text = Convert.ToString(friend2.getsetIntelligence);
        }
        else if (state.memType == "member3")
        {
            charKE.text = Convert.ToString(friend3.getsetKnowledgeEffect);
            charKL.text = Convert.ToString(friend3.getsetKnowledgeLevel);
            charIntel.text = Convert.ToString(friend3.getsetIntelligence);
        }
        else
        {
            return;
        }
    }

    //EQUIPMENT LIST
    public void refreshCharacterEquipmentList() 
    {
        //main character
        if(state.memType == "main character") 
        {
            mainCharacterEquipments();
        }

        else if(state.memType == "member1")
        {
            irahWitzEquipments();
        }
        
        else if(state.memType == "member2")
        {
            yuukiBelleEquipments();
        }

        else if(state.memType == "member3") 
        {
            reeveOwunEquipments();
        }
    }

    //CHARACTER EQUIPMENTS
    private void mainCharacterEquipments() 
    {
        if (mcEquipment.getWeaponItem() != null)
        {
            charOffenseEquipmentName.text = mcEquipment.getWeaponItem().data.itemName;
            charOffenseSprite.sprite = mcEquipment.getWeaponItem().GetSprite();
            offenseRarity.sprite = mcEquipment.getWeaponItem().getRaritySprite();
            charOffenseSprite.gameObject.SetActive(true);
            offenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charOffenseEquipmentName.text = "";
            charOffenseSprite.sprite = null;
            offenseRarity.sprite = null;
            charOffenseSprite.gameObject.SetActive(false);
            offenseRarity.gameObject.SetActive(false);
        }

        if (mcEquipment.getArmorItem() != null)
        {
            charDefenseEquipmentName.text = mcEquipment.getArmorItem().data.itemName;
            charDefenseSprite.sprite = mcEquipment.getArmorItem().GetSprite();
            defenseRarity.sprite = mcEquipment.getArmorItem().getRaritySprite();
            charDefenseSprite.gameObject.SetActive(true);
            defenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charDefenseEquipmentName.text = "";
            charDefenseSprite.sprite = null;
            defenseRarity.sprite = null;
            charDefenseSprite.gameObject.SetActive(false);
            defenseRarity.gameObject.SetActive(false);
        }

        if (mcEquipment.getAccessoryItem() != null)
        {
            charAccessoryEquipmentName.text = mcEquipment.getAccessoryItem().data.itemName;
            charAccessorySprite.sprite = mcEquipment.getAccessoryItem().GetSprite();
            accessoryRarity.sprite = mcEquipment.getAccessoryItem().getRaritySprite();
            charAccessorySprite.gameObject.SetActive(true);
        }
        else
        {
            charAccessoryEquipmentName.text = "";
            charAccessorySprite.sprite = null;
            accessoryRarity.sprite = null;
            charAccessorySprite.gameObject.SetActive(false);
            accessoryRarity.gameObject.SetActive(false);
        }
    }
    private void irahWitzEquipments() 
    {
        if (memOneEquipment.getWeaponItem() != null)
        {
            charOffenseEquipmentName.text = memOneEquipment.getWeaponItem().data.itemName;
            charOffenseSprite.sprite = memOneEquipment.getWeaponItem().GetSprite();
            offenseRarity.sprite = memOneEquipment.getWeaponItem().getRaritySprite();
            charOffenseSprite.gameObject.SetActive(true);
            offenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charOffenseEquipmentName.text = "";
            charOffenseSprite.sprite = null;
            offenseRarity.sprite = null;
            charOffenseSprite.gameObject.SetActive(false);
            offenseRarity.gameObject.SetActive(false);
        }

        if (memOneEquipment.getArmorItem() != null)
        {
            charDefenseEquipmentName.text = memOneEquipment.getArmorItem().data.itemName;
            charDefenseSprite.sprite = memOneEquipment.getArmorItem().GetSprite();
            defenseRarity.sprite = memOneEquipment.getArmorItem().getRaritySprite();
            charDefenseSprite.gameObject.SetActive(true);
            defenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charDefenseEquipmentName.text = "";
            charDefenseSprite.sprite = null;
            defenseRarity.sprite = null;
            charDefenseSprite.gameObject.SetActive(false);
            defenseRarity.gameObject.SetActive(false);
        }

        if (memOneEquipment.getAccessoryItem() != null)
        {
            charAccessoryEquipmentName.text = memOneEquipment.getAccessoryItem().data.itemName;
            charAccessorySprite.sprite = memOneEquipment.getAccessoryItem().GetSprite();
            accessoryRarity.sprite = memOneEquipment.getAccessoryItem().getRaritySprite();
            charAccessorySprite.gameObject.SetActive(true);
        }
        else
        {
            charAccessoryEquipmentName.text = "";
            charAccessorySprite.sprite = null;
            accessoryRarity.sprite = null;
            charAccessorySprite.gameObject.SetActive(false);
            accessoryRarity.gameObject.SetActive(false);
        }
    }
    private void yuukiBelleEquipments() 
    {
        if (memTwoEquipment.getWeaponItem() != null)
        {
            charOffenseEquipmentName.text = memTwoEquipment.getWeaponItem().data.itemName;
            charOffenseSprite.sprite = memTwoEquipment.getWeaponItem().GetSprite();
            offenseRarity.sprite = memTwoEquipment.getWeaponItem().getRaritySprite();
            charOffenseSprite.gameObject.SetActive(true);
            offenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charOffenseEquipmentName.text = "";
            charOffenseSprite.sprite = null;
            offenseRarity.sprite = null;
            charOffenseSprite.gameObject.SetActive(false);
            offenseRarity.gameObject.SetActive(false);
        }

        if (memTwoEquipment.getArmorItem() != null)
        {
            charDefenseEquipmentName.text = memTwoEquipment.getArmorItem().data.itemName;
            charDefenseSprite.sprite = memTwoEquipment.getArmorItem().GetSprite();
            defenseRarity.sprite = memTwoEquipment.getArmorItem().getRaritySprite();
            charDefenseSprite.gameObject.SetActive(true);
            defenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charDefenseEquipmentName.text = "";
            charDefenseSprite.sprite = null;
            defenseRarity.sprite = null;
            charDefenseSprite.gameObject.SetActive(false);
            defenseRarity.gameObject.SetActive(false);
        }

        if (memTwoEquipment.getAccessoryItem() != null)
        {
            charAccessoryEquipmentName.text = memTwoEquipment.getAccessoryItem().data.itemName;
            charAccessorySprite.sprite = memTwoEquipment.getAccessoryItem().GetSprite();
            accessoryRarity.sprite = memTwoEquipment.getAccessoryItem().getRaritySprite();
            charAccessorySprite.gameObject.SetActive(true);
        }
        else
        {
            charAccessoryEquipmentName.text = "";
            charAccessorySprite.sprite = null;
            accessoryRarity.sprite = null;
            charAccessorySprite.gameObject.SetActive(false);
            accessoryRarity.gameObject.SetActive(false);
        }
    }
    private void reeveOwunEquipments()
    {
        if (memThreeEquipment.getWeaponItem() != null)
        {
            charOffenseEquipmentName.text = memThreeEquipment.getWeaponItem().data.itemName;
            charOffenseSprite.sprite = memThreeEquipment.getWeaponItem().GetSprite();
            offenseRarity.sprite = memThreeEquipment.getWeaponItem().getRaritySprite();
            charOffenseSprite.gameObject.SetActive(true);
            offenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charOffenseEquipmentName.text = "";
            charOffenseSprite.sprite = null;
            offenseRarity.sprite = null;
            charOffenseSprite.gameObject.SetActive(false);
            offenseRarity.gameObject.SetActive(false);
        }

        if (memThreeEquipment.getArmorItem() != null)
        {
            charDefenseEquipmentName.text = memThreeEquipment.getArmorItem().data.itemName;
            charDefenseSprite.sprite = memThreeEquipment.getArmorItem().GetSprite();
            defenseRarity.sprite = memThreeEquipment.getArmorItem().getRaritySprite();
            charDefenseSprite.gameObject.SetActive(true);
            defenseRarity.gameObject.SetActive(true);
        }
        else
        {
            charDefenseEquipmentName.text = "";
            charDefenseSprite.sprite = null;
            defenseRarity.sprite = null;
            charDefenseSprite.gameObject.SetActive(false);
            defenseRarity.gameObject.SetActive(false);
        }

        if (memThreeEquipment.getAccessoryItem() != null)
        {
            charAccessoryEquipmentName.text = memThreeEquipment.getAccessoryItem().data.itemName;
            charAccessorySprite.sprite = memThreeEquipment.getAccessoryItem().GetSprite();
            accessoryRarity.sprite = memThreeEquipment.getAccessoryItem().getRaritySprite();
            charAccessorySprite.gameObject.SetActive(true);
        }
        else
        {
            charAccessoryEquipmentName.text = "";
            charAccessorySprite.sprite = null;
            accessoryRarity.sprite = null;
            charAccessorySprite.gameObject.SetActive(false);
            accessoryRarity.gameObject.SetActive(false);
        }
    }
}
