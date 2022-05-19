using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform[] point;

    public GameObject[] doors;

    public GameObject[] randomWalls;

    public GameObject[] randomEnvironment;

    public NavMeshReBake reBake;
    void Start()
    {
        
        randomWalls[Random.Range(0, randomWalls.Length)].SetActive(false);
        Debug.Log("Случайная стена  " + Random.Range(0, randomWalls.Length));
        int x = Random.Range(0, randomEnvironment.Length);
        int y = Random.Range(0, randomEnvironment.Length);
        int z = Random.Range(0, randomEnvironment.Length);
        while (x != y && x != z && y != z)
        {
            y = Random.Range(0, randomEnvironment.Length);
            z = Random.Range(0, randomEnvironment.Length);
        }
        randomEnvironment[x].SetActive(true);
        randomEnvironment[y].SetActive(true);
        randomEnvironment[z].SetActive(true);
        reBake = FindObjectOfType<NavMeshReBake>();
        reBake.Bake();

    }

    
    void Update()
    {
        
    }
}
