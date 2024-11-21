using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    int mapSize = 3;
    [SerializeField] float roomScale = 40f;

    [SerializeField] private GameObject GameMapMesh;

    // roomsdict
    public static Dictionary<Vector2, RoomBase> RoomsDict = new (); // coord : room dict

    // prefabs
    [SerializeField] private RoomBase[] RoomPrefabs;

    // navmesh
    private NavMeshSurface navMesh;

    public void CreateMap()
    {
        GameObject mapMesh = Instantiate(GameMapMesh, transform);

        int roomIndexer = 0;

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector2 coords = new Vector2(x * roomScale, y * roomScale); 
                RoomBase roomInstance;

                roomInstance = Instantiate(RoomPrefabs[roomIndexer], transform);
                roomInstance.Coords = coords;
                roomInstance.SetRoomLocation(roomInstance.Coords);
                RoomsDict.Add(coords, roomInstance); // add new room to dict for every possible coord pair

                roomIndexer += 1;
            }

        }

        CreateNavMesh();

    }

    private void CreateNavMesh()
    {
        navMesh = GetComponent<NavMeshSurface>();
       
        navMesh.BuildNavMesh();
    }
}
