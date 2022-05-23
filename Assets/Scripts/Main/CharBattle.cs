using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBattle : BattleUnit
{
    [SerializeField]
    public float speed;


    public bool active;
    public CharStateIs switcher;
    public ClickReceiver clickReceiver;
    public Vector3 finalPoint;
    public Vector3 cursorPoint;
    public TurnBaser turnBaser;
    public Outline outline;
    void Start()
    {
        clickReceiver = GetComponent<ClickReceiver>();
        clickReceiver = FindObjectOfType<ClickReceiver>();
        turnBaser = GetComponent<TurnBaser>();
        turnBaser = FindObjectOfType<TurnBaser>();
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (active)
        {
            switch (switcher)
            {
                case CharStateIs.Start:
                    {
                        FindPath(speed);
                        switcher = CharStateIs.Move;
                    }
                    break;
                case CharStateIs.Move://нужно сделать расходование очков движения
                    {
                        if (Input.GetMouseButton(0))
                        {
                            Move(cursorPoint, speed, out finalPoint);
                        }
                        if ((Mathf.Abs(transform.position.x - finalPoint.x) < 0.6f && Mathf.Abs(transform.position.z - finalPoint.z) < 0.6f)) 
                            switcher = CharStateIs.Ability;
                    }
                    break;
                case CharStateIs.Ability:
                    {
                        finalPoint = new Vector3(-100,-100,-100);
                        turnBaser.timeToNext = true;
                    }
                    break;
            }
        }
        if (!active)
        {
            DeletePoints();
            outline.OutlineWidth = 0;
        }
        else outline.OutlineWidth = 2;
    }
}
public enum CharStateIs
{
    Start,
    Move,
    Ability
}
