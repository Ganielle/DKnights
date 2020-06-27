using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemyBattleMovement : MonoBehaviour
{

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody enemyRigidBody;
    [SerializeField] private float rotationSpeed;


    GameObject player;
    BattleStates battleStates;
    Quaternion deltaRotation;

    private void Awake()
    {
        this.battleStates = GameManager.instance.battleStates;
    }

    private void Update()
    {
        enemyLookAtPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void enemyLookAtPlayer()
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            deltaRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            enemyRigidBody.rotation = Quaternion.Slerp(enemyRigidBody.rotation, deltaRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
