using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public LiveUnit[] liveUnits;
    public ClickReceiver[] clickReceivers;
    public Vector3 cursorCoords;
    public Material materialSet;
    public Material materialStart;

    public void Start()
    {
        liveUnits = FindObjectsOfType<LiveUnit>();
        materialStart = gameObject.GetComponentInChildren<MeshRenderer>().material;
        Debug.Log(liveUnits.Length);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        int flag = 0;
        for (int i = 0; i < liveUnits.Length; i++)
        {
            if ((Mathf.Abs(liveUnits[i].transform.position.x - gameObject.transform.position.x) < 0.6f && Mathf.Abs(liveUnits[i].transform.position.z - gameObject.transform.position.z) < 0.6f))
            {
                flag++;
            }
        }
        if (flag > 0)
        {
            for (int i = 0; i < liveUnits.Length; i++)
            {
                liveUnits[i].cursorPoint = new Vector3(100, 100, 100);
            }
            Debug.Log("низя");
            gameObject.GetComponentInChildren<MeshRenderer>().material = materialStart;
        }
        else
        {
            for (int i = 0; i < liveUnits.Length; i++)
            {
                liveUnits[i].cursorPoint = gameObject.transform.position;
            }
            Debug.Log("можно");
            gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = materialStart;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //gameObject.GetComponentInChildren<MeshRenderer>().material = materialSet;
    }


}
