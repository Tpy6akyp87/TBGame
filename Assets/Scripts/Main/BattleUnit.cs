using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : LiveUnit
{
    //public int health;
    // Start is called before the first frame update
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
        ReceiveDamage(dmg); //��������
        //target.health -= dmg;
        Debug.Log("������ �   " + target + "      " + target.health + "    ��������");
    }
}
