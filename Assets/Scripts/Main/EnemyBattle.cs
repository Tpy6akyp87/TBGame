using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EnemyBattle : BattleUnit, IPointerEnterHandler, IPointerExitHandler
{
    public LiveUnit[] liveUnits;
    public TurnBaser turnBaser;
    public EnemyStateIs switcher;
    public Vector3 finalPoint;
    public ClickReceiver clickReceiver;
    //public LiveUnit target;



    void Start()
    {
        liveUnits = FindObjectsOfType<LiveUnit>();
        clickReceiver = GetComponent<ClickReceiver>();
        outline = GetComponent<Outline>();
        turnBaser = GetComponent<TurnBaser>();
        turnBaser = FindObjectOfType<TurnBaser>();
    }
    // findtrget - move - doDamage
    void Update()
    {
        if (active)
        {
            switch (switcher)
            {
                case EnemyStateIs.Start:
                    {
                        outline.OutlineColor = Color.red;
                        outline.OutlineWidth = 2;
                        switcher = EnemyStateIs.Move;
                    }
                    break;
                case EnemyStateIs.Move://нужно сделать расходование очков движения
                    {
                        FindTarget(out target);
                        FindWayPoint(target, out clickReceiver);
                        Move(clickReceiver.transform.position, speed, out finalPoint);
                        if ((Mathf.Abs(transform.position.x - finalPoint.x) < 0.9f && Mathf.Abs(transform.position.z - finalPoint.z) < 0.9f))
                            switcher = EnemyStateIs.Ability;
                    }
                    break;
                case EnemyStateIs.Ability:
                    {
                        DeletePoints();
                        DoDamage(target, 1);
                        switcher = EnemyStateIs.Next;
                    }
                    break;
                case EnemyStateIs.Next:
                    {
                        target = null;
                        turnBaser.timeToNext = true;
                        finalPoint = new Vector3(-100, -100, -100);
                        switcher = EnemyStateIs.Start;
                    }
                    break;
            }
        }
    }
    public void FindTarget(out LiveUnit target)
    {
        for (int i = 0; i < turnBaser.friendlyBattleUnit.Length; i++)
        {
            turnBaser.friendlyBattleUnit[i].MyWeight((gameObject.transform.position - turnBaser.friendlyBattleUnit[i].transform.position).magnitude);
        }


        CharBattle temp;
        for (int i = 0; i < turnBaser.friendlyBattleUnit.Length - 1; i++)
        {
            for (int j = i+1; j < turnBaser.friendlyBattleUnit.Length; j++)
            {
                if (turnBaser.friendlyBattleUnit[i].weght > turnBaser.friendlyBattleUnit[j].weght)
                {
                    temp = turnBaser.friendlyBattleUnit[i];
                    turnBaser.friendlyBattleUnit[i] = turnBaser.friendlyBattleUnit[j];
                    turnBaser.friendlyBattleUnit[j] = temp;
                }    
                    //turnBaser.friendlyBattleUnit[i].health = turnBaser.friendlyBattleUnit[i].health + turnBaser.friendlyBattleUnit[j].health - (turnBaser.friendlyBattleUnit[j].health = turnBaser.friendlyBattleUnit[i].health);
            }
        }
        target = turnBaser.friendlyBattleUnit[turnBaser.friendlyBattleUnit.Length - 1];
        Debug.Log(target);
    }
    public void FindWayPoint(LiveUnit target, out ClickReceiver wayPoint)
    {
        wayPoint = null;
        float[] minDistanceToTarget = new float[turnBaser.clickReceivers.Length];
        float[] minDToObj = new float[9];
        ClickReceiver temp;

        for (int i = 0; i < turnBaser.clickReceivers.Length; i++)
        {
            minDistanceToTarget[i] = (turnBaser.clickReceivers[i].transform.position - target.transform.position).magnitude;
        }

        for (int i = 0; i < minDistanceToTarget.Length - 1; i++)
        {
            for (int j = i + 1; j < minDistanceToTarget.Length; j++)
            {
                if (minDistanceToTarget[i] > minDistanceToTarget[j])
                {
                    minDistanceToTarget[i] = minDistanceToTarget[i] + minDistanceToTarget[j] - (minDistanceToTarget[j] = minDistanceToTarget[i]);
                    temp = turnBaser.clickReceivers[i];
                    turnBaser.clickReceivers[i] = turnBaser.clickReceivers[j];
                    turnBaser.clickReceivers[j] = temp;
                }
            }
        }
        for (int i = 1; i < 10; i++)
        {
            minDToObj[i-1] = (turnBaser.clickReceivers[i].transform.position - gameObject.transform.position).magnitude;
        }
        for (int i = 0; i < 8; i++)
        {
            for (int j = i+1; j < 9; j++)
            {
                if (minDToObj[i] > minDToObj[j])
                {
                    minDToObj[i] = minDToObj[i] + minDToObj[j] - (minDToObj[j] = minDToObj[i]);
                    temp = turnBaser.clickReceivers[i+1];
                    turnBaser.clickReceivers[i+1] = turnBaser.clickReceivers[j+1];
                    turnBaser.clickReceivers[j+1] = temp;
                }
            }
        }
        
        for (int j = 1; j < 10; j++)
        {
            int flag = 0;
            for (int i = 0; i < liveUnits.Length; i++)
            {
                if ((Mathf.Abs(liveUnits[i].transform.position.x - turnBaser.clickReceivers[j].transform.position.x) < 0.6f && Mathf.Abs(liveUnits[i].transform.position.z - turnBaser.clickReceivers[j].transform.position.z) < 0.6f))
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                wayPoint = turnBaser.clickReceivers[j];
                break;
            }
        }
        
        //wayPoint = turnBaser.clickReceivers[1];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 2;
        targeted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targeted = false;
        outline.OutlineWidth = 0;
    }
}
public enum EnemyStateIs
{
    Start,
    Move,
    Ability,
    Next
}
