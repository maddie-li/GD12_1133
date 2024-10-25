using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    int mapSize = 3;

    [SerializeField] int roomScale = 5;

    public static Dictionary<Vector2, RoomBase> RoomsDict = new(); // coord : room dict

    [SerializeField] private RoomBase[] RoomPrefabs;

    private enum doorStates
    {
        open,
        closed,
        invisOpen,
        invisClosed
    }

    public void CreateMap()
    {

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector2 coords = new Vector2(x * roomScale, y * roomScale);
                RoomsDict.Add(coords, new RoomBase(coords)); // add new room to dict for every possible coord pair
            }
        }

    }

    public void VisualiseMap()
    {
        foreach (var roomInDict in RoomsDict)
        {
            var mapRoomVisualise = GameObject.CreatePrimitive(PrimitiveType.Cube); // create cube
            mapRoomVisualise.transform.position = new Vector3(roomInDict.Key.x, 0, roomInDict.Key.y); // location coords
            mapRoomVisualise.transform.localScale = new Vector3(0.9f, 0.5f, 0.9f); // small so can see spaces between
        }
    }
}
