using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanel : MonoBehaviour
{
    public CharBattle blond;
    public Text hpBlond;
    public CharBattle dark;
    public Text hpDark;
    public CharBattle bold;
    public Text hpBold;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharHpToText(blond, hpBlond);
        CharHpToText(dark, hpDark);
        CharHpToText(bold, hpBold);
    }
    public void CharHpToText(CharBattle charBattle, Text text)
    {
        text.text = charBattle.health.ToString();
    }
}
