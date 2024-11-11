using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;
    [SerializeField] private PlayerController PlayerPrefab;

    [SerializeField] private UI_Manager uiManager;

    private MapManager gameMap;
    private PlayerController playerController;

    [SerializeField] Vector3 playerStartPos;

    [SerializeField] bool makeMap;

    public void OnStartGame()
    {
        transform.position = Vector3.zero;

        uiManager.ActivateHUD();
        Debug.Log("GameManager: ActivatedHUD");

        SetupMap();
        SpawnPlayer();

    }

    public void SetupMap()
    {
        // create instance of map manager
        gameMap = Instantiate(GameMapPrefab);
        gameMap.transform.position = Vector3.zero;


        if (makeMap)
        {
            gameMap.CreateMap();
        }

    }

    public void SpawnPlayer()
    {
        // create player
        playerController = Instantiate(PlayerPrefab);
        playerController.transform.position = playerStartPos;
        playerController.SetUIReference(uiManager);

    }

    

}
