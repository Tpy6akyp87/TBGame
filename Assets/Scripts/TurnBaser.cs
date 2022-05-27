using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaser : MonoBehaviour
{
    public CharBattle[] friendlyBattleUnit;
    public LiveUnit[] battleunits;
    public bool timeToNext = false;
    public int turnNumber = 0;
    void Start()
    {
        friendlyBattleUnit = FindObjectsOfType<CharBattle>();
        battleunits = FindObjectsOfType<LiveUnit>();
        for (int i = 0; i < battleunits.Length; i++)
        {
            Debug.Log(battleunits[i]);
        }
        ActivateChar(turnNumber);
    }

    public void Update()//нужно сделать взврат к первому ходившему
    {
        if (timeToNext && turnNumber < battleunits.Length)
        {
            DeactivateChar(turnNumber);
            turnNumber++;
            if (turnNumber == battleunits.Length)
                turnNumber = 0;
            ActivateChar(turnNumber);
            timeToNext = false;
        }
        
    }
    public void ActivateChar(int i)
    {
        battleunits[i].active = true;
        //Debug.Log(battleunits[i]);
    }
    public void DeactivateChar(int i) 
    {
        battleunits[turnNumber].active = false;
        battleunits[turnNumber].outline.OutlineWidth = 0;
    }
}
