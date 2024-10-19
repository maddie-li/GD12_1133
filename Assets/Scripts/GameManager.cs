using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Map _gameMap;
    private BasePlayer _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameMap = new Map();
        _player = new BasePlayer(new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // temporary keypress (should probably use input manager)

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { _player.Move(Direction.n); }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) { _player.Move(Direction.e); }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { _player.Move(Direction.s); }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) { _player.Move(Direction.w); }
            

    }
}
