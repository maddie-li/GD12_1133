using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMapManager : MonoBehaviour
{
    /*int mapSize = 3;
    [SerializeField] int roomScale = 40;

    // roomsdict
    public static Dictionary<Vector2, RoomBase> RoomsDict = new (); // coord : room dict

    // roomtypes
    List<Vector2> combatRoomCoords = new List<Vector2>()
            {
                new Vector2(0, 1),
                new Vector2(1,0),
                new Vector2(1, 0),
                new Vector2(2, 2),
                new Vector2(1, 2)
            };

    List<Vector2> treasureRoomCoords = new List<Vector2>()
            {
                new Vector2 (0, 2),
                new Vector2 (2, 0),
            };

    // prefabs
    [SerializeField] private RoomBase[] RoomPrefabs;

    public void CreateMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector2 coords = new Vector2(x * roomScale, y * roomScale);
                RoomBase roomInstance;

                if (treasureRoomCoords.Contains(coords/roomScale))
                {
                    roomInstance = Instantiate(RoomPrefabs[1], transform);
                }
                else if (combatRoomCoords.Contains(coords / roomScale))
                {
                    roomInstance = Instantiate(RoomPrefabs[2], transform);
                }
                else
                {
                    roomInstance = Instantiate(RoomPrefabs[0], transform);

                }

                roomInstance.Coords = coords;
                roomInstance.RoomExits = new bool[] { false, true, false, true };

                roomInstance.SetRoomLocation(roomInstance.Coords);
                // roomInstance.SetExits(roomInstance.RoomExits);

                RoomsDict.Add(coords, roomInstance); // add new room to dict for every possible coord pair
            }

        }

    }*/
}
