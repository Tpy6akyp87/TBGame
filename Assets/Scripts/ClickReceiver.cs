using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public MoveShit[] shit;
    public CharBattle[] charBattles;
    public Vector3 cursorCoords;
    public Material materialSet;
    public Material materialStart;

    public void Start()
    {
        shit = FindObjectsOfType<MoveShit>();
        charBattles = FindObjectsOfType<CharBattle>();
        materialStart = gameObject.GetComponentInChildren<MeshRenderer>().material;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //for (int i = 0; i < shit.Length; i++)
        //{
        //    shit[i].targetToGo = gameObject.transform.position;
        //}
        for (int i = 0; i < charBattles.Length; i++)
        {
            charBattles[i].cursorPoint = gameObject.transform.position;
        }
        //cursorCoords = gameObject.transform.position;
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialStart;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //for (int i = 0; i < shit.Length; i++)
        //{
        //    shit[i].targetToGo = gameObject.transform.position;
        //}
        //cursorCoords = gameObject.transform.position;
        //Debug.Log(cursorCoords);
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
    }


}
