using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaser : MonoBehaviour
{
    public MoveShit[] shits;
    public LiveUnit[] battleunits;
    public bool timeToNext = false;
    public int turnNumber = 0;
    void Start()
    {
        shits = FindObjectsOfType<MoveShit>();
        battleunits = FindObjectsOfType<CharBattle>();
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
        //battleunits[i].switcher = CharStateIs.Start;
        battleunits[i].active = true;
    }
    public void DeactivateChar(int i) 
    {
        battleunits[turnNumber].active = false;
        battleunits[turnNumber].outline.OutlineWidth = 0;
    }
}
