using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPauseFunctions : MonoBehaviour
{
    [SerializeField]private GameObject StatusMenu, equipmentMenu;

    public void showStatusMenu()
    {
        gameObject.SetActive(false);
        StatusMenu.SetActive(true);
    }

    public void showEquipmentMenu()
    {
        gameObject.SetActive(false);
        equipmentMenu.SetActive(true);
    }

}
