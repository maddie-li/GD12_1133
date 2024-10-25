using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer 
{
    //  LOCATION
    public Vector2 Coords;
    public BaseRoom CurrentRoom;

    // VISUALS
    GameObject playerVisualise = GameObject.CreatePrimitive(PrimitiveType.Cylinder); // create player

    // FUNCTIONS
    public void Move(Direction dir)
    {
        // update room
        BaseRoom _newRoom;
        _newRoom = Map.FindRoom(Coords, dir);
        CurrentRoom = _newRoom;

        // update coords
        Coords = CurrentRoom.Coords;

        // update visuals
        
        playerVisualise.transform.position =  new Vector3(Coords.x, 0.5f, Coords.y); // location coords

    }

    // CONSTRUCTOR
    public BasePlayer(Vector2 coords)
    {
        this.Coords = coords;
        this.CurrentRoom = Map.RoomsDict[coords];

        playerVisualise.transform.position = new Vector3(coords.x, 0.5f, coords.y); // location coords
        playerVisualise.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // location coords
    }

}
