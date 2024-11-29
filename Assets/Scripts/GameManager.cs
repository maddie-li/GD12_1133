using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    [SerializeField] private PlayerController PlayerPrefab;
    [SerializeField] private UI_Manager CanvasPrefab;
    [SerializeField] private CombatManager CombatPrefab;

    private MapManager gameMap;
    private UI_Manager uiManager;
    private CombatManager combatManager;
    private PlayerController playerController;
    private CombatantInfo playerInfo;

    public bool isInCombat = false;

    [SerializeField] Vector3 playerStartPos;

    [SerializeField] private string playerName;
    [SerializeField] private int playerHealth;
    [SerializeField] private List<Item> playerInventory;

    // SETUP
    public void Start()
    {
        // INSTANTIATE UIMANAGER
        uiManager = Instantiate(CanvasPrefab, transform);
        uiManager.SetGameManager(this); // assign reference
        uiManager.ActivateMainMenu(); // set UI to main menu
        
    }

    public void OnStartButton()
    {
        DontDestroyOnLoad(gameObject);

        transform.position = Vector3.zero;

        SceneManager.LoadScene("MainLevel");

        SetupMap();
        SpawnPlayer();

        uiManager.ActivateHUD(); // set UI to HUD

        // INSTANTIATE COMBATMANAGER
        SetupCombatManager();

    }

    public void SetupMap()
    {
        // INSTANTIATE MAPMANAGER
        gameMap = Instantiate(GameMapPrefab, transform);
        gameMap.transform.position = Vector3.zero;

        gameMap.SetUIManager(uiManager);

        // CREATE MAP 
        gameMap.CreateMap();

    }

    public void SpawnPlayer()
    {
        // setup info
        playerInfo = new CombatantInfo(playerName, playerHealth,playerInventory);
        uiManager.SetPlayerInfo(playerInfo); // assign reference
        Debug.LogWarning("PlayerInfo being assigned to UI Manager");

        // create player
        playerController = Instantiate(PlayerPrefab, transform);
        playerController.transform.position = playerStartPos;
        playerController.SetReferences(uiManager, playerInfo);

    }



    private void SetupCombatManager()
    {
        combatManager = Instantiate(CombatPrefab, transform);
        combatManager.SetManagers(this, uiManager, playerInfo); // assign reference
        uiManager.SetCombatManager(combatManager);

        GameObject[] enemiesInLevel = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in enemiesInLevel)
        {
            if (obj.TryGetComponent<EnemyAI>(out EnemyAI enemyAI))
            {
                enemyAI.SetManagers(combatManager, playerController);
            }
        }
    }

    // GAME STATE
    public void PauseGame()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        Debug.Log("GAMEMANAGER: Paused");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SlowGame()
    {
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        Debug.Log("GAMEMANAGER: Slowed");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        Debug.Log("GAMEMANAGER: Resumed");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void LoseGame()
    {

        uiManager.ActivateDeath();
        playerController.Die();

        Destroy(playerController.gameObject, 5f);

        //PauseGame();

    }

    public void ExitGame()
    {
        Debug.LogWarning("QUIT GAME");
        Application.Quit();

    }

}
