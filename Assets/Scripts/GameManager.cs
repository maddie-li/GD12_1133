using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    [SerializeField] private PlayerController PlayerPrefab;

    private MapManager _gameMap;
    private PlayerController _playerController;

    [SerializeField] Vector3 playerStartPos;

    [SerializeField] bool makeMap;

<<<<<<< Updated upstream
    public void Start()
    {
        Debug.Log("GameManager: Start");
=======
    public int enemyCount;

    // SETUP
    public void Start()
    {
        // INSTANTIATE UIMANAGER
        uiManager = Instantiate(CanvasPrefab, transform);
        uiManager.SetGameManager(this); // assign reference
        uiManager.ActivateMainMenu(); // set UI to main menu

        enemyCount = 1;
        
    }

    private void Update()
    {
        Debug.LogWarning(enemyCount);

        if (enemyCount == 0)
        {
            WinGame();
        }
    }

    public void OnStartButton()
    {
        DontDestroyOnLoad(gameObject);

>>>>>>> Stashed changes
        transform.position = Vector3.zero;

        SetupMap();
        SpawnPlayer();

    }

    public void SetupMap()
    {
        Debug.Log("GameManager: SetupMap");
        // create instance of map manager
        _gameMap = Instantiate(GameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;


        if (makeMap)
        {
            _gameMap.CreateMap();
        }

        Debug.Log("GameManager: Done SetupMap");
    }

    public void SpawnPlayer()
    {
        Debug.Log("GameManager: SpawnPlayer");

        // create player
        _playerController = Instantiate(PlayerPrefab, transform);

        _playerController.transform.position = playerStartPos;

        Debug.Log("GameManager: Done SpawnPlayer");
    }

<<<<<<< Updated upstream
    
=======


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

        enemyCount = enemiesInLevel.Length / 2;
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

    public void WinGame()
    {
        Debug.LogError("WON GAME!");
    }

    public void ExitGame()
    {
        Debug.LogWarning("QUIT GAME");
        Application.Quit();

    }

}
