using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemPlayerAnimator : MonoBehaviour
{
    [SerializeField] private pickUpItemsAnimation pickup;

    public void PickupItems()
    {
        pickup.QueueItems();
    }
}
