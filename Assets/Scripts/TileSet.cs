using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TileSet : MonoBehaviour
{
    public GameObject tile;
    public GameObject wall;
    public GameObject column;
    public int tilesetWight;
    public int tilesetLenght;
    private int rnd; 
    public float offSet;
    private Vector3 startPosition = Vector3.zero;
    public Vector3 movePosition;
    public Vector3[,] tileCoords;
    public Vector3[,] tileK;
    public NavMeshReBake reBake;

    public void Start()
    {
        reBake = FindObjectOfType<NavMeshReBake>();
        CreateTileSet(tilesetLenght, tilesetWight, startPosition, offSet, tile, column, wall, out tileK);
        //reBake.Bake();
        
    }
    public void CreateTileSet(int dlina, int shirina, Vector3 nachalo, float otstup, GameObject kvadrat, GameObject stena, GameObject kolonna, out Vector3[,] tileK)
    {
        tileK = new Vector3[dlina, shirina];
        tileCoords = new Vector3[dlina, shirina];
        for (int i = 0; i < dlina; i++)
        {
            for (int j = 0; j < shirina; j++)
            {
                if (i == 0 || j == 0 || i == dlina - 1 || j == shirina - 1)
                {
                    GameObject newtile = Instantiate(stena, nachalo + new Vector3(otstup * j, 0.0f, otstup * i), stena.transform.rotation) as GameObject;
                    newtile.transform.SetParent(gameObject.transform);
                }
                else
                {
                    rnd = Random.Range(1, 8);
                    if (rnd == 3)
                    {
                        GameObject newtile = Instantiate(kolonna, nachalo + new Vector3(otstup * j, 0.0f, otstup * i), kolonna.transform.rotation) as GameObject;
                        newtile.transform.SetParent(gameObject.transform);
                    }
                    else
                    {
                        GameObject newtile = Instantiate(kvadrat, nachalo + new Vector3(otstup * j, 0.0f, otstup * i), kvadrat.transform.rotation) as GameObject;
                        newtile.transform.SetParent(gameObject.transform);
                    }
                }

                
                tileCoords[i, j] = nachalo + new Vector3(otstup * j, 0.0f, 0.0f);
                tileK[i, j] = tileCoords[i, j];
                //GameObject newPoint = Instantiate(tochka, tileCoords[i, j], tochka.transform.rotation) as GameObject;
            }
            //nachalo += new Vector3(0.0f, 0.0f, otstup);
        }
        //Debug.Log("собрал зону");
        reBake.Bake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
