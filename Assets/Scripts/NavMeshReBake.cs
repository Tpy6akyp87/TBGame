using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshReBake : MonoBehaviour
{
    
    public void Bake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
        //Debug.Log("Запек");
    }
}
