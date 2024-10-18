using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map 
{
    int mapSize = 3;

    BaseRoom[,] RoomArray;
    readonly Dictionary<Vector2, BaseRoom> _rooms = new();

    public Map()
    {
        CreateMap();
        VisualiseMap();
    }


    public void CreateMap()
    {
        RoomArray = new BaseRoom[mapSize, mapSize];

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector2 coords = new Vector2(x, y);
                _rooms.Add(coords, new BaseRoom(coords));
            }
        }

        foreach (var roomInDict in _rooms)
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
        foreach(var roomInDict in _rooms)
        {
            var mapRoomVisualise = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mapRoomVisualise.transform.position = new Vector3(roomInDict.Key.x, 0, roomInDict.Key.y);
        }
        
    }

    private BaseRoom FindRoom(Vector2 currentRoomCoords, Direction dir)
    {
        BaseRoom room = null;
        Vector2 nextRoomCoords = new Vector2(-1, -1);

        switch (dir)
        {
            case Direction.n:
                // Determine North Room
                nextRoomCoords = currentRoomCoords + Vector2.up;
                break;
            case Direction.e:
                // east
                nextRoomCoords = currentRoomCoords + Vector2.right;
                break;
            case Direction.s:
                // south
                nextRoomCoords = currentRoomCoords + Vector2.down;
                break;
            case Direction.w:
                // west
                nextRoomCoords = currentRoomCoords + Vector2.left;
                break;
        }

    }
