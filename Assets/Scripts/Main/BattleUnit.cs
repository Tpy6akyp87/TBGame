using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : LiveUnit
{
    public int strestrength;
    public int agility;
    public int intellect;
    public int wisdom;
    public float distanceTo;
    public int actionIn;

    public float weght;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoDamage(LiveUnit target, int dmg) 
    {
        //Debug.Log("����� ���� ��     " + target);
        target.ReceiveDamage(dmg); //��������
        //target.health -= dmg;
        Debug.Log("������ �   " + target + "      " + target.health + "    ��������");
    }
}
