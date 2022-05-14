using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MoveShit : MonoBehaviour, IPointerClickHandler
{
    public Camera mainCamera;
    public NavMeshAgent agent;
    public Vector3 targetToGo;
    public float clickDistance;
    public float possibleDistance;
    public float maxDistance;
    public bool canGo;
    public ClickReceiver[] tilesToGo;
    public GameObject pointToGo;
    private List<GameObject> pointS = new List<GameObject>();
    public Animator animator;
    public bool active;
    public Outline outline;
    public float upScale;

    public void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
        active = false;

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public int State
    {
        get { return animator.GetInteger("State"); }
        set { animator.SetInteger("State", value); }
    }
    public void Update()
    {
        if (Input.GetMouseButton(2))
        {
            active = false;
            outline.OutlineWidth = 0;
        }
        if (Input.GetMouseButton(0) && active)
        {
            State = 1;
            DeletePoints();
            Move();
            State = 1;
        }
        if (Input.GetMouseButton(1) && active)
        {
            DeletePoints();
            tilesToGo = FindObjectsOfType<ClickReceiver>();
            Debug.Log(tilesToGo.Length);
            for (int t = 0; t < tilesToGo.Length; t++)
            {
                FindPath(tilesToGo[t]);
            }
        }
        if ((Mathf.Abs(transform.position.x - targetToGo.x) < 0.6f && Mathf.Abs(transform.position.z - targetToGo.z) < 0.6f))
        {
            State = 0;
        }
        if (!active)
        {
            DeletePoints();
            outline.OutlineWidth = 0;
        }
        else
            outline.OutlineWidth = 2;
    }
    public void Move()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, targetToGo, NavMesh.AllAreas, navMeshPath))
        {
            clickDistance = Vector3.Distance(transform.position, navMeshPath.corners[1]);
            for (int i = 1; i < navMeshPath.corners.Length - 1; i++)
            {
                clickDistance += Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i + 1]); ;
            }

            if (clickDistance <= maxDistance)
            {
                agent.SetDestination(targetToGo);
                State = 1;
            }
        }
    }
    public void FindPath(ClickReceiver tileToCheck) 
    {
        NavMeshPath findPath = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, tileToCheck.transform.position, NavMesh.AllAreas, findPath) && (Mathf.Abs(transform.position.x - tileToCheck.transform.position.x) > 0.6f || Mathf.Abs(transform.position.z - tileToCheck.transform.position.z) > 0.6f)) // не ищет путь по отрицательным координатам, спотыкается о свое текущее положение(исключить текущий тайл из обхода
        {
            Debug.Log(tileToCheck.transform.position);
            possibleDistance = Vector3.Distance(transform.position, findPath.corners[1]);
            for (int i = 1; i < findPath.corners.Length - 1; i++)
            {
                possibleDistance += (findPath.corners[i] - findPath.corners[i + 1]).magnitude; //Vector3.Distance(findPath.corners[i], findPath.corners[i + 1]);
            }
            if (possibleDistance <= maxDistance)
            {
                GameObject newPoint = Instantiate(pointToGo, tileToCheck.transform.position + new Vector3(0,upScale,0), tileToCheck.transform.rotation);
                pointS.Add(newPoint);
            }
        }
        
    }
    public void DeletePoints()
    {
        for (int i = 0; i < pointS.Count; i++)
        {
            Destroy(pointS[i]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active = true;
        outline.OutlineWidth = 2;
    }
}
