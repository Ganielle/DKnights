using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float suspicionTime = 3f;

    [Header("NavMeshAgent")]
    [SerializeField] private NavMeshAgent enemyNavMesh;

    GameObject player;
    Quaternion deltaRotation;
    BattleStates battleStates;
    float DistanceToPlayer;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    private void Awake()
    {
        battleStates = GameManager.instance.battleStates;

        battleStates.battleStateChange += stopNavMesh;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        enemyNavMesh.speed = runSpeed;

        guardPosition = transform.position;
    }

    private void FixedUpdate()
    {
        enemyMove();
    }


    private void stopNavMesh(object sender, EventArgs e)
    {
        if(battleStates.getsetBattleState != BattleStates.BattleState.NONE)
        {
            enemyNavMesh.velocity = Vector3.zero;
            enemyNavMesh.Stop();
        }
    }

    private void enemyMove()
    {
        
        if(enemyChaseRadius() && battleStates.getsetBattleState == BattleStates.BattleState.NONE)
        {
            timeSinceLastSawPlayer = 0;
            AttackBehaviour();
        }
        else if(timeSinceLastSawPlayer < suspicionTime)
        {
            //Suspicion State
            SuspicionBehaviour();
        }
        else
        {
            GuardBehaviour();
        }

        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void AttackBehaviour()
    {
        enemyNavMesh.destination = player.transform.position;
    }

    private void SuspicionBehaviour()
    {
        enemyNavMesh.destination = Vector3.zero;
    }

    private void GuardBehaviour()
    {
        enemyNavMesh.destination = guardPosition;
    }


    private bool enemyChaseRadius()
    {
        DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        return DistanceToPlayer < chaseDistance;
    }
}
