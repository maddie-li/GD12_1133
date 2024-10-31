using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MapManager GameMapPrefab;

    private MapManager _gameMap;

    public void Start()
    {
        Debug.Log("GameManagerStart");
        transform.position = Vector3.zero;

        // create instance of map manager
        _gameMap = Instantiate(GameMapPrefab, transform);
        _gameMap.transform.position = Vector3.zero;

        // create the map
        _gameMap.CreateMap();

        Debug.Log("Map created");

    }
   
}
