using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.UI.Extensions.Tweens;

public class pickUpItemsAnimation : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator itemAnim;

    [Header("Text Mesh Pro")]
    [SerializeField] private TextMeshProUGUI itemName;

    [Header("Image")]
    [SerializeField] private Image itemType;
    [SerializeField] private Image rareType;

    private SoundManager sfx;

    private PickUpItems pickUpItems;
    private ItemPickupCommand currentItemQueue;
    public Queue<ItemPickupCommand> itemQueue = new Queue<ItemPickupCommand>();
    Inventory inventory;

    private void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        pickUpItems = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpItems>();
        this.inventory = GameManager.instance.inventory;
    }

    private void Update() => ProcessQueue();

    public void PlayTingSound() => sfx.playPickUpItemSound();

    public void StopShowIndicator() => currentItemQueue.StopIndicatorAnimation();

    #region PICK UP ITEMS
    private void ProcessQueue()
    {
        if (currentItemQueue != null && currentItemQueue.IsFinished == false)
            return;

        if (itemQueue.Any() == false)
            return;

        currentItemQueue = itemQueue.Dequeue();
        currentItemQueue.Execute();
    }

    private void PickItem()
    {
        pickUpItems.itemData.isEquip = false;

        if (pickUpItems.itemData.data.itemType == item.ItemType.staff)
        {
            inventory.addItemStaff(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.wand)
        {
            inventory.addItemWand(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.orb)
        {
            inventory.addItemOrb(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.book)
        {
            inventory.addItemBook(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.defense)
        {
            inventory.addItemDefense(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.accessory)
        {
            inventory.addItemAccessory(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.questItems)
        {
            inventory.addQuestItem(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.keyItems)
        {
            inventory.addKeyItem(pickUpItems.itemData);
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.manaPotion || pickUpItems.itemData.data.itemType == item.ItemType.healthPotion)
        {
            if (inventory.GetItemConsumableList().Count == 0)
            {
                inventory.addItemConsumable(pickUpItems.itemData);
            }
            else
            {
                foreach (ItemData itemInventory in inventory.GetItemConsumableList())
                {
                    if (pickUpItems.itemData.data.itemID == itemInventory.data.itemID)
                    {
                        itemInventory.itemAmount += itemInventory.data.howMany;
                    }
                    else
                    {
                        inventory.addItemConsumable(pickUpItems.itemData);
                    }
                }
            }
        }
        else if (pickUpItems.itemData.data.itemType == item.ItemType.money)
        {
            inventory.addMoney(pickUpItems.itemData.moneyValue);
        }
        else
        {
            return;
        }
        Destroy(pickUpItems.gameObjectItem);
    }

    public void QueueItems()
    {
        if (pickUpItems.itemData == null)
            return;
        itemQueue.Enqueue(item: new PickItemQueue(pickUpItems.itemData, itemName, itemType, rareType, itemAnim));
        PickItem();
        GameManager.instance.isPickingUp = false;
    }

    #endregion
}

internal class PickItemQueue : ItemPickupCommand
{
    private readonly ItemData fieldItems;
    private bool isFinishedAnimating = true;

    TextMeshProUGUI itemName;

    Image itemType;
    Image rareType;

    Animator anim;

    public PickItemQueue(ItemData fieldItems, TextMeshProUGUI itemName, Image itemType, Image rareType, Animator anim)
    {
        this.fieldItems = fieldItems;
        this.itemName = itemName;
        this.itemType = itemType;
        this.rareType = rareType;
        this.anim = anim;

    }

    public override bool IsFinished => isFinishedAnimating;

    public override void Execute()
    {
        PlayAnimation();
    }


    public void PlayAnimation()
    {

        if (fieldItems != null)
        {
            isFinishedAnimating = false;
            itemName.text = fieldItems.data.itemName + " x " + Convert.ToString(fieldItems.data.howMany);
            itemType.sprite = fieldItems.GetSprite();
            rareType.sprite = fieldItems.getRaritySprite();

            ShowIndicatorAnimation();
        }
    }

    private void ShowIndicatorAnimation() => anim.SetTrigger("PlayShowIndicator");

    public override void StopIndicatorAnimation()
    {
        itemName.text = "";
        itemType.sprite = null;
        rareType.sprite = null;
        isFinishedAnimating = true;
    }
}
