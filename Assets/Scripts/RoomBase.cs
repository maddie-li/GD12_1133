using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    public Vector2 Coords;
    public bool[] RoomExits;

    // [SerializeField] private GameObject NorthWall, EastWall, SouthWall, WestWall;

    public void SetRoomLocation(Vector2 coords)
    {
        transform.position = new Vector3(coords[0], 0, coords[1]); // location coords
    }

    /*public void SetExits(bool[] roomExits)
    {
        // doors to iterate through
        GameObject[] RoomSides= { NorthWall, EastWall, SouthWall, WestWall };

        // to match doors with bools
        int arrayIndex = 0;

        foreach (GameObject side in RoomSides)
        {
            // get door from roomside
            side.transform.GetChild(0).gameObject.SetActive(roomExits[arrayIndex]);

            arrayIndex += 1;
        }
        
    }*/


    public void OnRoomEnter()
    {
        Debug.Log($"Entering {name}!");
    }

    public void OnRoomExit()
    {
        Debug.Log($"Exiting {name}!");
    }

    public void SearchRoom()
    {
        Debug.Log($"Searching {name}!");
    }

    public void Update()
    {
        
    }

}
