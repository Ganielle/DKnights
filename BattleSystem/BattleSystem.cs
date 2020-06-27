using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.EventSystems;

public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Values")]
    [SerializeField] private float camHeight;
    [SerializeField] private float camDistance;
    [SerializeField] private float battleFieldExpandSpeed;
    [SerializeField] private float battleFieldMaxSize;
    [SerializeField] private float battleFieldHeight;
    [SerializeField] private float battleFieldPositionHeight;
    [SerializeField] private float waitFirstAttackerStateTime, showOverviewCamTime, showBattleGUI;
    [SerializeField] private float lookAtSpeedRotation;

    [Header("Game Objects UI")]
    [SerializeField] private GameObject battleOneUI;
    [SerializeField] private GameObject GameControllerUI;

    [Header("Game Objects Battle Field")]
    [SerializeField] private GameObject battleField;
      
    [Header("Game Objects")]
    [SerializeField] private List<GameObject> attackGUI;
    [SerializeField] private GameObject selectEnemyGUI, backButton;
    [SerializeField] private GameObject player;

    [Header("Game Object Cinemachine")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject showBattleBannerCam;
    [SerializeField] private GameObject actionCamGo, overviewBattleCam, playerCam;

    [Header("Rect Transform")]
    [SerializeField] private RectTransform selectEnemyRect;
    [SerializeField] private RectTransform backButtonRect;
    [SerializeField] private RectTransform[] attackGUIRect;

    [Header("Cinemachine")]
    [SerializeField] private CinemachineVirtualCamera actionVirtualCam;
    [SerializeField] private CinemachineVirtualCamera overviewBattleVirtualCam;

    [Header("Scripts")]
    [SerializeField] private List<MovingRectTransform> battleGUI;
    [SerializeField] private List<MovingRectTransform> moveSelectEnemyGUI;
    [SerializeField] private MovingRectTransform description;
    [SerializeField] private BattleSoundsActivateAnimation battleSfx;

    GameObject enemyFirst;
    Vector3 lookPos, selectEnemyOldPos, backButtonOldPos;
    Vector3[] attackOldPos;
    BattleStates battleStates;
    Party party;
    Queue battleQueue;
    actionSelected actionSelect;

    private void Start()
    {
        this.battleStates = GameManager.instance.battleStates;
        this.party = GameManager.instance.party;
        this.actionSelect = GameManager.instance.actionSelect;
        this.battleStates.battleStateChange += onBattleStateChange;
        this.battleStates.methodStateChange += onMethodStateChange;
        this.actionSelect.selectedEnemyChange += onSelectedEnemyChange;

        battleQueue = new Queue();
        battleField.transform.localScale = new Vector3(0, battleFieldHeight, 0);

        battleField.SetActive(false);
        actionCamGo.SetActive(false);
        overviewBattleCam.SetActive(false);
        playerCam.SetActive(true);
    }

    private void Update()
    {
        changeFirstState();

        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
            StartCoroutine(setBattleFieldSize());

        
        rotateOverviewBattleCam();
    }

    private void mainBattleSystemOne()
    {
        if (GameControllerUI.activeSelf && battleStates.getsetBattleState != BattleStates.BattleState.NONE)
        {
            GameControllerUI.SetActive(false);

            playerCam.SetActive(false);
            showBattleBannerCam.SetActive(true);

            showBattleBannerCam.transform.position = mainCamera.transform.position;
            showBattleBannerCam.transform.rotation = mainCamera.transform.rotation;
        }


        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            battleField.SetActive(true);

            enemyFirst = battleStates.getEnemyGameObjects()[0];

            battleOneUI.SetActive(true);
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.FIGHTSTART)
        {
            StartCoroutine(checkFirstAttacker());
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.PLAYERTURN)
        {
            activatePlayerTurnCamera();
            StartCoroutine(showGUIPlayersTurn());
        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERONETURN)
        {

        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERTWOTURN)
        {

        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.MEMBERTHREETURN)
        {

        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.ENEMYTURN)
        {

        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.VICTORY)
        {

        }
        else if (battleStates.getsetBattleState == BattleStates.BattleState.DEFEAT)
        {

        }

        if (battleStates.getsetBattleState == BattleStates.BattleState.CHOOSEENEMY)
        {

        }
    }

    #region EVENTS

    private void onSelectedEnemyChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        if (actionSelect.SelectedEnemy != null)
            overviewBattleVirtualCam.LookAt = actionSelect.SelectedEnemy.transform;

        if (actionSelect.lastSelectedEnemy != null)
            actionSelect.lastSelectedEnemy.GetComponent<moveYLoop>().stopLeanTweenIndicator();

    }

    private void onMethodStateChange(object sender, EventArgs e)
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        if (battleStates.getsetMethodState == BattleStates.MethodState.CHANT)
            chooseEnemy();
        else if (battleStates.getsetMethodState == BattleStates.MethodState.SELECT)
            description.moveRectTransform();
    }

    private void onBattleStateChange(object sender, EventArgs e)
    {

        if (battleStates.getsetBattleState == BattleStates.BattleState.NONE)
            return;

        mainBattleSystemOne();

        StartCoroutine(showOverviewCamera());
        attackGUIOldPos();

        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            battleField.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + battleFieldPositionHeight, player.transform.position.z);

        }
    }

    #endregion

    #region CAMERA SETTINGS
    IEnumerator showOverviewCamera()
    {
        yield return new WaitForSeconds(showOverviewCamTime);
        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            //set up the camera
            overviewBattleCam.transform.position = new Vector3(player.transform.position.x + UnityEngine.Random.Range(20, camDistance),
                player.transform.position.y + camHeight, player.transform.position.z + camDistance);

            overviewBattleVirtualCam.LookAt = enemyFirst.transform;
            lookPos = enemyFirst.transform.position - player.transform.position;

            showBattleBannerCam.SetActive(false);
            overviewBattleCam.SetActive(true);
        }
    }

    private void rotateOverviewBattleCam()
    {
      
        if (battleStates.getsetMethodState == BattleStates.MethodState.CHANT || battleStates.getsetMethodState == BattleStates.MethodState.SELECTENEMY ||
            battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            overviewBattleCam.transform.RotateAround(lookPos, Vector3.up, lookAtSpeedRotation * Time.deltaTime);
        }
    }

    private void activatePlayerTurnCamera()
    {
        overviewBattleCam.SetActive(false);
        actionCamGo.SetActive(true);

        actionVirtualCam.transform.position = player.transform.position;
        actionVirtualCam.LookAt = player.transform;
    }
    #endregion

    #region STATE SETTINGS
    private void changeFirstState()
    {
        if (battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE && battleStates.getsetPlayerBattlePositionState == BattleStates.BattlePosition.DONE)
            battleStates.getsetBattleState = BattleStates.BattleState.FIGHTSTART;

    }

    IEnumerator checkFirstAttacker()
    {
        yield return new WaitForSeconds(waitFirstAttackerStateTime);
        if (battleStates.getsetFirstTurn == BattleStates.FirstTurn.PLAYER)
        {
            battleStates.getsetBattleState = BattleStates.BattleState.PLAYERTURN;
        }
        else if (battleStates.getsetFirstTurn == BattleStates.FirstTurn.ENEMY)
        {
            battleStates.getsetBattleState = BattleStates.BattleState.ENEMYTURN;
        }
    }

    #endregion

    #region GUI SETTINGS

    IEnumerator showGUIPlayersTurn()
    {
        yield return new WaitForSeconds(showBattleGUI);
        if (battleStates.getsetBattleState == BattleStates.BattleState.PLAYERTURN)
        {
            foreach (MovingRectTransform move in battleGUI)
            {
                move.moveRectTransform();
            }
            battleSfx.playClickSound();
        }
    }

    private void chooseEnemy()
    {
        actionCamGo.SetActive(false);
        overviewBattleCam.SetActive(true);


        foreach (GameObject gameobject in attackGUI)
        {
            gameobject.SetActive(false);
        }

        resetAttackGUIPos();
        selectEnemyGUIOldPos();
        selectEnemyGUI.SetActive(true);
        backButton.SetActive(true);
        foreach (MovingRectTransform rect in moveSelectEnemyGUI)
        {
            rect.moveRectTransform();
        }

    }

    public void reShowBattleGUI()
    {

        selectEnemyGUI.SetActive(false);
        backButton.SetActive(false);
        overviewBattleCam.SetActive(false);
        actionCamGo.SetActive(true);
        foreach (GameObject gameobject in attackGUI)
        {
            gameobject.SetActive(true);
        }
        foreach (MovingRectTransform rect in battleGUI)
        {
            rect.moveRectTransform();
        }

        resetSelectEnemyGUIPos();
    }

    private void selectEnemyGUIOldPos()
    {
        selectEnemyOldPos = new Vector3(selectEnemyRect.localPosition.x, selectEnemyRect.localPosition.y, selectEnemyRect.localPosition.z);
        backButtonOldPos = new Vector3(backButtonRect.localPosition.x, backButtonRect.localPosition.y, backButtonRect.localPosition.z);
    }

    private void resetSelectEnemyGUIPos()
    {
        selectEnemyRect.localPosition = new Vector3(selectEnemyOldPos.x, selectEnemyOldPos.y, selectEnemyOldPos.z);
        backButtonRect.localPosition = new Vector3(backButtonOldPos.x, backButtonOldPos.y, backButtonOldPos.z);
    }

    private void attackGUIOldPos()
    {
        if(battleStates.getsetBattleState == BattleStates.BattleState.STARTBATTLE)
        {
            for (int a = 0; a < attackGUIRect.Length; a++)
            {
                attackOldPos = new Vector3[a];
            }
            for (int a = 0; a < attackOldPos.Length; a++)
            {
                attackOldPos[a] = new Vector3(attackGUIRect[a].localPosition.x, attackGUIRect[a].localPosition.y, attackGUIRect[a].localPosition.z);
            }
        }
    }

    private void resetAttackGUIPos()
    {
        for (int a = 0; a < attackOldPos.Length; a++)
        {
            attackGUIRect[a].localPosition = new Vector3(attackOldPos[a].x, attackOldPos[a].y, attackOldPos[a].z);
        }
    }

    #endregion

    #region BATTLE FIELD MESH SETTINGS
    IEnumerator setBattleFieldSize()
    {
        yield return new WaitForSeconds(showOverviewCamTime);
        if (battleField.transform.localScale.x < battleFieldMaxSize || battleField.transform.localScale.z < battleFieldMaxSize)
        {
            battleField.transform.localScale += new Vector3(1 * battleFieldExpandSpeed * Time.deltaTime, 0, 1 * battleFieldExpandSpeed * Time.deltaTime);
        }
    }

    #endregion
}
