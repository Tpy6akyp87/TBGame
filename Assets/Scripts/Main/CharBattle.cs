using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharBattle : BattleUnit, IPointerEnterHandler, IPointerExitHandler
{   
    public Camera mainCamera;
    public CharStateIs switcher;
    public Vector3 finalPoint;
    public TurnBaser turnBaser;
    void Start()
    {
        target = null;
        mainCamera = Camera.main;
        turnBaser = GetComponent<TurnBaser>();
        turnBaser = FindObjectOfType<TurnBaser>();
        outline = GetComponent<Outline>();
        switcher = CharStateIs.Start;
    }

    void Update()
    {
        if (active)
        {
            switch (switcher)
            {
                case CharStateIs.Start:
                    {
                        outline.OutlineColor = Color.green;
                        outline.OutlineWidth = 2;
                        targeted = false;
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
                        DeletePoints();
                        if (Input.GetMouseButton(0))
                        {
                            FindTarget();
                            if (target != null)
                            {
                                DoDamage(target, 1);
                            }
                            switcher = CharStateIs.Next;
                        }
                    }
                    break;
                case CharStateIs.Next:
                    {
                        target = null;
                        if (Input.GetMouseButton(1))
                        {
                            turnBaser.timeToNext = true;
                            finalPoint = new Vector3(-100, -100, -100);
                            switcher = CharStateIs.Start;
                        }
                    }
                    break;
            }
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!active)
        {
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 2;
            targeted = true;
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!active)
        {
            targeted = false;
            outline.OutlineWidth = 0;
        }
    }
    public void FindTarget() 
    {
        for (int i = 0; i < turnBaser.battleunits.Length; i++)
        {
            if (turnBaser.battleunits[i].targeted)
            {
                target = turnBaser.battleunits[i];
                Debug.Log("Нашёл цель:   " + target);
            }
        }
    }
}
public enum CharStateIs
{
    Start,
    Move,
    Ability,
    Next
}
