using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : LiveUnit
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoDamage(LiveUnit target, int dmg) 
    {
        Debug.Log("����� ���� ��     " + target);
        target.ReceiveDamage(dmg); //��������
        //target.health -= dmg;
        Debug.Log("������ �   " + target + "      " + target.health + "    ��������");
    }
}
