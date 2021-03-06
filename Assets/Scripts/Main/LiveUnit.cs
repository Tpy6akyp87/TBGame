using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class LiveUnit : Unit
{
    public int health;
    public float speed;

    public NavMeshAgent agent;
    public GameObject pointToGo; 
    public float upScale;//временная поднималка подсветки возможного пути
    private List<GameObject> pointS = new List<GameObject>();
    public bool mouseOnMe;
    public bool targeted;
    public bool active;
    public LiveUnit target;
    public Outline outline;
    public Vector3 myPosition;
    public Vector3 cursorPoint;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        outline = GetComponent<Outline>();
    }
    void Update()
    {
        if (health < 1)
        {
            Die();
        }
    }
    public void Move(Vector3 targetToGo, float maxDistance, out Vector3 finalPoint)
    {
        finalPoint = targetToGo;
        NavMeshPath navMeshPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, targetToGo, NavMesh.AllAreas, navMeshPath))
        {
            float clickDistance = Vector3.Distance(transform.position, navMeshPath.corners[1]);
            for (int i = 1; i < navMeshPath.corners.Length - 1; i++)
            {
                clickDistance += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i + 1]); ;
            }

            if (clickDistance <= maxDistance)
            {
                DeletePoints();
                agent.SetDestination(targetToGo);
            }
        }
    }
    public void FindPath(float maxDistance)
    {
        DeletePoints();
        ClickReceiver[] tilesToGo;
        tilesToGo = FindObjectsOfType<ClickReceiver>();
        for (int t = 0; t < tilesToGo.Length; t++)
        {
            NavMeshPath findPath = new NavMeshPath();
            if (NavMesh.CalculatePath(transform.position, tilesToGo[t].transform.position, NavMesh.AllAreas, findPath) && (Mathf.Abs(transform.position.x - tilesToGo[t].transform.position.x) > 0.6f || Mathf.Abs(transform.position.z - tilesToGo[t].transform.position.z) > 0.6f)) 
            {
                float possibleDistance = Vector3.Distance(transform.position, findPath.corners[1]);
                for (int i = 1; i < findPath.corners.Length - 1; i++)
                {
                    possibleDistance += (findPath.corners[i] - findPath.corners[i + 1]).magnitude; //Vector3.Distance(findPath.corners[i], findPath.corners[i + 1]);
                }
                if (possibleDistance <= maxDistance)
                {
                    GameObject newPoint = Instantiate(pointToGo, tilesToGo[t].transform.position + new Vector3(0, upScale, 0), tilesToGo[t].transform.rotation);
                    pointS.Add(newPoint);
                }
            }
        }
        

    }
    public void ReceiveDamage(int dmg) 
    {
        health -= dmg;
    }

    public void DeletePoints()
    {
        for (int i = 0; i < pointS.Count; i++)
        {
            Destroy(pointS[i]);
        }
    }


    
}
