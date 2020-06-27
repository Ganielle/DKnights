using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerBattleMovement : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] private float delayMovement;
    [SerializeField] private float dashDistance, dashSpeed, rotationSpeed;
    [SerializeField] private float waitChangeBattlePosState;

    [Header("RigidBody and Transform")]
    [SerializeField] private Rigidbody playerRigidBody;

    [Header("Animation")]
    [SerializeField] private Animator boyAnim;
    [SerializeField] private Animator girlAnim;

    Animator playerAnimator;
    AnimatorStateInfo stateInfo;

    int idle = Animator.StringToHash("Base Layer.Battle Locomotion.idle");
    int initiateBattle = Animator.StringToHash("Base Layer.Battle Locomotion.initiateBattle");
    int showWeapon = Animator.StringToHash("Base Layer.Battle Locomotion.showWeapon");
    int battleIdle = Animator.StringToHash("Base Layer.Battle Locomotion.battleIdle");

    checkGender isBoy;
    Party party;
    BattleStates battleStates;
    Quaternion deltaRotation;
    Vector3 waypoint;


    private void Start()
    {
        this.battleStates = GameManager.instance.battleStates;
        this.isBoy = GameManager.instance.gender;
        this.party = GameManager.instance.party;

        this.isBoy.genderOnChange += onGenderChange;
        this.battleStates.battleStateChange += onBattleStateChange;
        checkGenderAnimation();
    }

    private void onBattleStateChange(object sender, EventArgs e)
    {

        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            playerAnimator.SetTrigger("isStartBattle");
            waypoint = playerRigidBody.position + (-playerRigidBody.transform.forward * dashDistance);
            deltaRotation = Quaternion.LookRotation(waypoint - playerRigidBody.position);
        }

    }

    private void onGenderChange(object sender, EventArgs e)
    {
        checkGenderAnimation();
    }

    private void FixedUpdate()
    {
        stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);


        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
            StartCoroutine(startRotationBattle());

        if(stateInfo.fullPathHash == initiateBattle)
        {
            playerMovePosition();
        }

    }


    IEnumerator startRotationBattle()
    {
        yield return new WaitForSeconds(delayMovement);

        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            playerRigidBody.rotation = Quaternion.Slerp(playerRigidBody.rotation, deltaRotation, rotationSpeed * Time.deltaTime);
            playerAnimator.SetTrigger("initiateBattle");
        }
        yield break;
    }

    private void playerMovePosition()
    {

        if(Vector3.Distance(playerRigidBody.position, battleStates.getEnemyGameObjects()[0].transform.position) < dashDistance)
            playerRigidBody.MovePosition(Vector3.MoveTowards(playerRigidBody.position, waypoint, dashSpeed * Time.deltaTime));

        else if (Vector3.Distance(playerRigidBody.position, battleStates.getEnemyGameObjects()[0].transform.position) > dashDistance)
        {
            deltaRotation = Quaternion.LookRotation(battleStates.getEnemyGameObjects()[0].transform.position - playerRigidBody.position);
            playerRigidBody.rotation = Quaternion.Slerp(playerRigidBody.rotation, deltaRotation, rotationSpeed * Time.deltaTime);
            playerAnimator.SetTrigger("showWeapon");
            StartCoroutine(setPlayerBattlePositionState());
        }
    }

    IEnumerator setPlayerBattlePositionState()
    {
        if (party.GetPartyMembers().Count == 0)
            yield return null;

        yield return new WaitForSeconds(waitChangeBattlePosState);

        if (party.GetPartyMembers().Count == 1)
            battleStates.getsetPlayerBattlePositionState = BattleStates.BattlePosition.DONE;
    }

    private void checkGenderAnimation()
    {
        if (isBoy.getsetBoyGirlChecker)
        {
            playerAnimator = boyAnim;
        }
        else
        {
            playerAnimator = girlAnim;
        }
    }
}
