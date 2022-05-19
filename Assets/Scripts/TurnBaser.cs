using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaser : MonoBehaviour
{
    public MoveShit[] shits;
    public bool timeToNext = false;
    public int turnNumber = 0;
    void Start()
    {
        shits = FindObjectsOfType<MoveShit>();
        NextChar(turnNumber);
    }

    public void Update()//нужно сделать взврат к первому ходившему
    {
        if (timeToNext && turnNumber < shits.Length)
        {
            shits[turnNumber].active = false;
            turnNumber++;
            NextChar(turnNumber);
            timeToNext = false;
        }
    }
    public void NextChar(int i)
    {
        shits[i].switcher = StateIs.Start;
        shits[i].active = true;
    }
}
