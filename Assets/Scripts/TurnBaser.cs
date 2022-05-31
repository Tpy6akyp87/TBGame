using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBaser : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    public Button button;
    public CharBattle[] friendlyBattleUnit;
    public LiveUnit[] battleunits;
    public ClickReceiver[] clickReceivers;
    public bool timeToNext = false;
    public int turnNumber = 0;
    void Start()
    {
        clickReceivers = FindObjectsOfType<ClickReceiver>();
        Debug.Log(clickReceivers.Length);
        friendlyBattleUnit = FindObjectsOfType<CharBattle>();
        battleunits = FindObjectsOfType<LiveUnit>();
        ActivateChar();
        for (int i = 0; i < battleunits.Length; i++)
        {
            Button newbutton = Instantiate(button, gameObject.transform);
            buttons.Add(newbutton);
            newbutton.GetComponentInChildren<Text>().text = battleunits[i].name;
        }

    }

    public void Update()
    {
        if (timeToNext && turnNumber < battleunits.Length)
        {
            DeactivateChar(turnNumber);
            NextTurn(turnNumber);
            turnNumber++;
            if (turnNumber == battleunits.Length)
                turnNumber = 0;
            Invoke("ActivateChar", 2.0f);
            //ActivateChar(turnNumber);
            timeToNext = false;
        }
        
        
    }
    public void ActivateChar()
    {
        battleunits[turnNumber].active = true;
        
    }
    public void DeactivateChar(int i) 
    {
        battleunits[turnNumber].active = false;
        battleunits[turnNumber].outline.OutlineWidth = 0;
    }
    public void NextTurn(int i)
    {
        Destroy(buttons[0].gameObject);
        buttons.RemoveAt(0);
        Button newbutton = Instantiate(button, gameObject.transform);
        buttons.Add(newbutton);
        newbutton.GetComponentInChildren<Text>().text = battleunits[i].name;
    }
}
