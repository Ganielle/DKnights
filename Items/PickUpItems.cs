using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{

    [HideInInspector] public ItemData itemData;
    [HideInInspector] public GameObject gameObjectItem;
    [SerializeField] private playerMovement movement;

    [SerializeField] private GameObject pickUpButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FieldItem"))
        {
            pickUpButton.SetActive(true);
            itemData = other.gameObject.GetComponent<ItemPickup>().fieldItems;
            gameObjectItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FieldItem"))
        {
            pickUpButton.SetActive(false);
            itemData = null;
            gameObjectItem = null;
        }
    }

    public void PlayPickUpItems()
    {
        GameManager.instance.isPickingUp = true;
        pickUpButton.SetActive(false);
        movement.anim.SetTrigger("pickItems");
    } 
}
