using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Jobs;
using Unity.Burst;

public class EquipmentItemList : MonoBehaviour
{

    [Header("Instantiate Game Object")]
    [SerializeField]private GameObject itemBar;
    [SerializeField]private GameObject equipmentWindow;


    GameObject itemList;

    Inventory inventory;
    UI_CharacterEquipmentSlot equipmentSlot;
    PauseStateMenu state;

    private void Awake()
    {
        this.state = GameManager.instance.pauseState;
        this.inventory = GameManager.instance.inventory;
    }

    public void RefreshInventoryItems()
    {
        //resets inventory list
        foreach(Transform child in equipmentWindow.transform)
        {
            Destroy(child.gameObject);
        }

        //Instantiate inventory based on
        //equipment state
        if(state.equipState == PauseStateMenu.EquipmentState.OFFENSE)
        {
            if(state.memType == "main character")
            {
                getItemList(Inventory.ItemType.staff);
            }
            else if(state.memType == "member1")
            {
                getItemList(Inventory.ItemType.wand);
            }
            else if(state.memType == "member2")
            {
                getItemList(Inventory.ItemType.orb);
            }
            else if(state.memType == "member3")
            {
                getItemList(Inventory.ItemType.book);
            }
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.DEFENSE)
        {
            getItemList(Inventory.ItemType.defense);
        }
        else if(state.equipState == PauseStateMenu.EquipmentState.ACCESSORY)
        {
            getItemList(Inventory.ItemType.accessory);
        }
    }

    //ITEMS
    private void getItemList(Inventory.ItemType itemType)
    {
        foreach(ItemData items in inventory.GetItems(itemType))   
        {
            itemList = Instantiate(itemBar, equipmentWindow.transform);
            Image image = itemList.GetComponent<RectTransform>().Find("itemIcon").GetComponent<Image>();
            TextMeshProUGUI itemNametext = itemList.GetComponent<RectTransform>().Find("itemName").GetComponent<TextMeshProUGUI>();
            Image rarityImage = itemList.GetComponent<RectTransform>().Find("rarity").GetComponent<Image>();
            image.sprite = items.GetSprite();
            rarityImage.sprite = items.getRaritySprite();
            itemNametext.text = items.data.itemName;
            equipmentSlot = itemList.GetComponent<UI_CharacterEquipmentSlot>();

            //PASSING OF VALUE IN UI_EQUIPMENTCHARACTERSLOT SCRIP
            equipmentSlot.Item = items;
        }
    }
}
