using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    public MoveShit shit;
    public Material materialSet; 
    public Material materialStart;

    public void OnPointerEnter(PointerEventData eventData)
    {
        shit.targetToGo = gameObject.transform.position;
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialStart;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        shit.targetToGo = gameObject.transform.position;
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
    }

    public void Start()
    {
        shit = FindObjectOfType<MoveShit>();
        materialStart = gameObject.GetComponentInChildren<MeshRenderer>().material;
    }
}
