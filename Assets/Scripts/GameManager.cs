using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    [SerializeField] private PlayerController PlayerPrefab;
    [SerializeField] private UI_Manager CanvasPrefab;
    [SerializeField] private GameObject PixelRendererPrefab;
    [SerializeField] private PlayerInfo playerInfo;

    private MapManager gameMap;
    private UI_Manager uiManager;
    private GameObject pixelRenderer;
    private PlayerController playerController;

    [SerializeField] Vector3 playerStartPos;

    [SerializeField] bool makeMap;

    public void Start()
    {

        uiManager = Instantiate(CanvasPrefab, transform);
       
        uiManager.SetManager(this);
        uiManager.ActivateMainMenu();

        pixelRenderer = Instantiate(PixelRendererPrefab, transform);
    }

    public void OnStartButton()
    {
        DontDestroyOnLoad(gameObject);

        transform.position = Vector3.zero;

        SceneManager.LoadScene("MainLevel");
        uiManager.ActivateHUD();
        SetupMap();
        SpawnPlayer();

    }

    public void SetupMap()
    {
        // create instance of map manager
        gameMap = Instantiate(GameMapPrefab, transform);
        gameMap.transform.position = Vector3.zero;


        if (makeMap)
        {
            gameMap.CreateMap();
        }

    }

    public void SpawnPlayer()
    {
        // create player
        playerController = Instantiate(PlayerPrefab, transform);
        playerController.transform.position = playerStartPos;
        playerController.SetReferences(uiManager, playerInfo);

    }

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
        Time.timeScale = 0.1f;
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

    public void ExitGame()
    {
        Debug.LogWarning("QUIT GAME");
        Application.Quit();

    }



}
