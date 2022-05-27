using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EnemyBattle : BattleUnit, IPointerEnterHandler, IPointerExitHandler
{
    public TurnBaser turnBaser;
    public EnemyStateIs switcher;
    public Vector3 finalPoint;
    public ClickReceiver clickReceiver;
    //public LiveUnit target;



    void Start()
    {
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
                case EnemyStateIs.Move://нужно сделать расходование очков движени€
                    {
                        FindTarget(out target);
                        Move(target.transform.position, speed, out finalPoint);
                        if ((Mathf.Abs(transform.position.x - finalPoint.x) < 0.6f && Mathf.Abs(transform.position.z - finalPoint.z) < 0.6f))
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
        //target = null;
        for (int i = 0; i < turnBaser.friendlyBattleUnit.Length - 1; i++)
        {
            for (int j = i+1; j < turnBaser.friendlyBattleUnit.Length; j++)
            {
                if (turnBaser.friendlyBattleUnit[i].health > turnBaser.friendlyBattleUnit[j].health)
                    turnBaser.friendlyBattleUnit[i].health = turnBaser.friendlyBattleUnit[i].health + turnBaser.friendlyBattleUnit[j].health - (turnBaser.friendlyBattleUnit[j].health = turnBaser.friendlyBattleUnit[i].health);
            }
        }
        target = turnBaser.friendlyBattleUnit[0];
        Debug.Log(target);
    }
    public void FindWayPoint(LiveUnit target)
    {
        float[] minDistanceToTarget = new float[clickReceiver.clickReceivers.Length];

        for (int i = 0; i < clickReceiver.clickReceivers.Length; i++)//заполн€ю массив расто€ний от тайлов до цели
        {
            minDistanceToTarget[i] = (clickReceiver.clickReceivers[i].transform.position - target.transform.position).magnitude;
        }

        for (int i = 0; i < minDistanceToTarget.Length - 1; i++)//сортирую по возрастанию 
        {
            for (int j = i + 1; j < minDistanceToTarget.Length; j++)
            {
                if (minDistanceToTarget[i] > minDistanceToTarget[j])
                    minDistanceToTarget[i] = minDistanceToTarget[i] + minDistanceToTarget[j] - (minDistanceToTarget[j] = minDistanceToTarget[i]);
            }
        }
        for (int i = 1; i < 10; i++)
        {
            //надо вернуть »ћ≈ЌЌќ нужный кликресивер
        }
        //найти кликресивер на минимальном рассто€нии от таргета
        //найти кликресивер на минимальном рассто€нии от геймобьекта
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
