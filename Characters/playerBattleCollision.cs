using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class playerBattleCollision : MonoBehaviour
{

    [Header("Animator")]
    [SerializeField] private Animator boyAnim;
    [SerializeField] private Animator girlAnim;

    [Header("Game Object")]
    [SerializeField] private GameObject battleSystem;

    Animator playerAnimator;
    BattleStates battleStates;
    List<GameObject> enemy;

    private void Start()
    {
        this.battleStates = GameManager.instance.battleStates;
        enemy = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemy.Add(col.gameObject);
            foreach (GameObject go in enemy)
            {
                battleStates.setEnemyGameObjects(go);
            }

            battleStates.getsetBattleState = BattleStates.BattleState.STARTBATTLE;
            battleStates.getsetFirstTurn = BattleStates.FirstTurn.PLAYER;

            battleSystem.SetActive(true);

            GetComponent<playerMovement>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
