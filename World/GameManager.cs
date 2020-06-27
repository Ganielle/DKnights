using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public PauseStateMenu pauseState = new PauseStateMenu();
    public PlayerStats playerStats = new PlayerStats();
    public friend1Stats friend1CharStats = new friend1Stats();
    public friend2Stats friend2CharStats = new friend2Stats();
    public friend3Stats friend3CharStats = new friend3Stats();
    public Party party = new Party();
    public partyMembers PartyMembers = new partyMembers();
    public Inventory inventory = new Inventory();
    public LevelSystem levelSystem = new LevelSystem();
    public itemSelected itemSelect = new itemSelected();
    public equipmentMC mcEquipment = new equipmentMC();
    public member1Equipment memOneEquipment = new member1Equipment();
    public member2Equipment memTwoEquipment = new member2Equipment();
    public member3Equipment memThreeEquipment = new member3Equipment();
    public BattleStates battleStates = new BattleStates();
    public checkGender gender = new checkGender();
    public BattleEventButtons battleButtons = new BattleEventButtons();
    public SceneChecker sceneChecker = new SceneChecker();
    public actionSelected actionSelect = new actionSelected();
    public DayTime dayTime = new DayTime();

    [HideInInspector] public bool isPickingUp;

    public static GameManager instance;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    float totalProgress;

    [Header("World Values")]
    [SerializeField] private float gravityStrength;

    [Header("Scene Load First")]
    [SerializeField] private string sceneFirstLoad;

    [Header("Game Object")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject loadingCamera, characterCamera, characterCameraCinemachine;

    [Header("Slider")]
    [SerializeField] private Slider loadingBar;


    private void Awake()
    {
        instance = this;

        //characterCamera.SetActive(false);
        //characterCameraCinemachine.SetActive(false);

        party.addParty(new partyMembers { memType = partyMembers.memberType.mainCharacter });


        Vector3 gravityS = new Vector3(0, gravityStrength, 0);
        Physics.gravity = gravityS;

        SceneManager.LoadSceneAsync(sceneFirstLoad, LoadSceneMode.Additive);
        sceneChecker.getsetSceneName = sceneFirstLoad;
    }

    #region GAME LOADER
    public void LoadGame()
    {
        characterCamera.SetActive(false);
        characterCameraCinemachine.SetActive(false);
        loadingCamera.SetActive(true);
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(GameDatabaseStatic.getsetPreviousScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(sceneChecker.getsetSceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalProgress += operation.progress;
                }

                totalProgress = (totalProgress / scenesLoading.Count) * 100f;

                loadingBar.value = Mathf.RoundToInt(totalProgress);


                yield return null;
            }
        }

        loadingScreen.SetActive(false);
        loadingCamera.SetActive(false);
        characterCamera.SetActive(true);
        characterCameraCinemachine.SetActive(true);
    }
    #endregion

    
}
