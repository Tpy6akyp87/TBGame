using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaser : MonoBehaviour
{
    public MoveShit[] shits;
    public CharBattle[] battleunits;
    public bool timeToNext = false;
    public int turnNumber = 0;
    void Start()
    {
        shits = FindObjectsOfType<MoveShit>();
        battleunits = FindObjectsOfType<CharBattle>();
        NextChar(turnNumber);
    }

    public void Update()//нужно сделать взврат к первому ходившему
    {
        if (timeToNext && turnNumber < shits.Length)
        {
            //shits[turnNumber].active = false;
            battleunits[turnNumber].active = false;

            turnNumber++;
            if (turnNumber == 3)
                turnNumber = 0;
            NextChar(turnNumber);
            timeToNext = false;
        }
        
    }
    public void NextChar(int i)
    {
        //shits[i].switcher = StateIs.Start;
        //shits[i].active = true;

        battleunits[i].switcher = CharStateIs.Start;
        battleunits[i].active = true;
    }
}
