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

    public void Update()//����� ������� ������ � ������� ���������
    {
        if (timeToNext && turnNumber < shits.Length)
        {
            Debug.Log(timeToNext);
            shits[turnNumber].active = false;
            turnNumber++;
            if (turnNumber == 3)
                turnNumber = 0;
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
