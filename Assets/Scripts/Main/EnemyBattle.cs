using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class EnemyBattle : BattleUnit, IPointerEnterHandler, IPointerExitHandler
{
    public TurnBaser turnBaser;




    void Start()
    {
        outline = GetComponent<Outline>();
        turnBaser = GetComponent<TurnBaser>();
        turnBaser = FindObjectOfType<TurnBaser>();
    }
    // findtrget - move - doDamage
    void Update()
    {
        
    }
    public void FindTarget(out LiveUnit target)
    {
        target = null;
        for (int i = 0; i < turnBaser.battleunits.Length - 1; i++)
        {
            for (int j = i+1; j < turnBaser.battleunits.Length; i++)
            {
                if (turnBaser.battleunits[i].health > turnBaser.battleunits[j].health)
                    turnBaser.battleunits[i].health = turnBaser.battleunits[i].health + turnBaser.battleunits[j].health - (turnBaser.battleunits[j].health = turnBaser.battleunits[i].health);
            }
            //turnBaser.battleunits.
            //if (turnBaser.battleunits[i].targeted)
            //{
            //    target = turnBaser.battleunits[i];
            //    Debug.Log("Нашёл цель:   " + target);
            //}
        }
        target = turnBaser.battleunits[0];
    }
    public void FindWayPoint(LiveUnit target)
    {
        //попробовать пойти к цели напрямую (по идее начнет толкаться)
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
