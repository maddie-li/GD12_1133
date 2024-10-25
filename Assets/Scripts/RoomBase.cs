using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    public Vector2 Coords;

    [SerializeField] private GameObject NorthDoor, EastDoor, SouthDoor, WestDoor;

    // CONSTRUCTOR
    public RoomBase(Vector2 coords)
    {
        this.Coords = Coords;
    }
}
