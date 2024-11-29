using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    public Vector2 Coords;
    private UI_Manager uiManager;
    [SerializeField] public string RoomName;

    public void SetUIManager(UI_Manager newUImanager)
    {
        uiManager = newUImanager;
    }

    public void SetRoomLocation(Vector2 coords)
    {
        transform.position = new Vector3(coords[0], 0, coords[1]); // location coords
    }

    public void OnRoomEnter()
    {
        //Debug.Log($"Entering {name}!");
        uiManager.roomText.text = RoomName;
    }

    public void OnRoomExit()
    {
        //Debug.Log($"Exiting {name}!");
        uiManager.roomText.text = "";
    }

    public void SearchRoom()
    {
        Debug.Log($"Searching {name}!");
    }

    public void Update()
    {
        
    }

}
