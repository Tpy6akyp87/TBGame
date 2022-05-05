using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public int rooms;
    public Transform Player;
    public Room[] RoomPrefab;
    public Room FirstRoom;
    private List<Room> spawnedRoms = new List<Room>();
    private int exitPoint;
    private int enterPoint;
    private List<int> oneORthree = new List<int>() { 0, 2 };
    void Start()
    {
        spawnedRoms.Add(FirstRoom);
        exitPoint = 0;
        for (int i = 0; i < rooms; i ++)
        {
            SpawnRoom();
            Debug.Log(exitPoint);
        }
    }

    
    void Update()
    {

    }
    private void SpawnRoom()
    {
        Room newRoom = Instantiate(RoomPrefab[Random.Range(0, RoomPrefab.Length)]);
        if (exitPoint == 0)
        {
            enterPoint = 3;
            newRoom.transform.position = spawnedRoms[spawnedRoms.Count - 1].point[exitPoint].position - newRoom.point[enterPoint].localPosition;
            exitPoint = Random.Range(0, 3);
        }
        else 
        if (exitPoint == 1)
        {
            enterPoint = 2;
            newRoom.transform.position = spawnedRoms[spawnedRoms.Count - 1].point[exitPoint].position - newRoom.point[enterPoint].localPosition;
            exitPoint = Random.Range(0, 2);
        }
        else
        if (exitPoint == 2)
        {
            enterPoint = 1;
            newRoom.transform.position = spawnedRoms[spawnedRoms.Count - 1].point[exitPoint].position - newRoom.point[enterPoint].localPosition;
            exitPoint = oneORthree[Random.Range(0,2)];
        }
        newRoom.doors[enterPoint].SetActive(false);
        newRoom.doors[exitPoint].SetActive(false);
        spawnedRoms.Add(newRoom);
    }
}
