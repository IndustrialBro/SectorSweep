using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GridDoohickey : MonoBehaviour
{
    public static GridDoohickey Instance { get; private set; }
    private GridDoohickey() { if(Instance == null)Instance = this; }

    [SerializeField]
    int xCount, yCount;
    [SerializeField]
    GameObject tilePrefab;

    List<Tile> tiles;
    private void Start()
    {
        Instance.tiles = new List<Tile>();
        SpawnGrid();
    }

    void SpawnGrid()
    {
        int temp = 0;
        Tile t = tilePrefab.GetComponent<Tile>();
        Vector3 pos;
        GameObject g;
        for (int i = 0; i < xCount; i++)
        {
            for(int j = 0; j < yCount; j++)
            {
                
                pos = new Vector3(t.size * i, 20, t.size * j);
                g = Instantiate(tilePrefab, pos, Quaternion.identity);
                
                t = g.GetComponent<Tile>();

                if (t.IsAboveGround())
                {
                    t.name = $"t{temp}";
                    Instance.tiles.Add(t);
                    temp++;
                }
                else
                {
                    Destroy(t.gameObject);
                }
            }
        }
    }

    public Tile RequestTile(string tileName)
    {
        foreach(Tile t in tiles)
        {
            if(t.name == tileName) return t;
        }
        return null;
    }
}
