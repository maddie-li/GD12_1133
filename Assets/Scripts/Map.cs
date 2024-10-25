using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I could use array in place of dictionary

public class Map
{
    int mapSize = 3;

    // BaseRoom[,] RoomArray; // rooms array
    public static Dictionary<Vector2, BaseRoom> RoomsDict = new(); // coord : room dict

    public Map()
    {
        CreateMap();

        VisualiseMap();
    }


    public void CreateMap()
    {

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector2 coords = new Vector2(x, y);
                RoomsDict.Add(coords, new BaseRoom(coords)); // add new room to dict for every possible coord pair
            }
        }

        foreach (var roomInDict in RoomsDict)
        {
            BaseRoom northRoom = FindRoom(roomInDict.Key, Direction.n);
            BaseRoom eastRoom = FindRoom(roomInDict.Key, Direction.e);
            BaseRoom southRoom = FindRoom(roomInDict.Key, Direction.s);
            BaseRoom westRoom = FindRoom(roomInDict.Key, Direction.w);

            roomInDict.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }

    }

    public void VisualiseMap()
    {
        foreach (var roomInDict in RoomsDict)
        {
            var mapRoomVisualise = GameObject.CreatePrimitive(PrimitiveType.Cube); // create cube
            mapRoomVisualise.transform.position = new Vector3(roomInDict.Key.x, 0, roomInDict.Key.y); // location coords
            mapRoomVisualise.transform.localScale = new Vector3 (0.9f, 0.5f, 0.9f); // small so can see spaces between
        }

    }

    public static BaseRoom FindRoom(Vector2 currentRoomCoords, Direction dir)
    {
        // initialise variables as null or nonexistent room to start
        BaseRoom room = null; 
        Vector2 nextRoomCoords = new Vector2(-1, -1); 

        switch (dir)
        {
            case Direction.n:
                nextRoomCoords = currentRoomCoords + Vector2.up; // increase y
                break;
            case Direction.e:
                nextRoomCoords = currentRoomCoords + Vector2.right; // increase x
                break;
            case Direction.s:
                nextRoomCoords = currentRoomCoords + Vector2.down; // decrease y
                break;
            case Direction.w:
                nextRoomCoords = currentRoomCoords + Vector2.left; // increase x
                break;
        }

        if (RoomsDict.TryGetValue(nextRoomCoords, out var nextRoom)) // try to use dict
        {
            room = nextRoom;
        }

        return room;

    }

}
