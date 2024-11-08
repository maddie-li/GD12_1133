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

    public void Start()
    {
        Debug.Log("GameManager: Start");
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

    

}
