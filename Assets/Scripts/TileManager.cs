using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public Vector3 SpawnPlace;
    public float tileLength;
    public int NumberOfTiles = 4;
    public Transform player;
    private int countDestroy = 0;

    private List<GameObject> activeTiles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<NumberOfTiles; i++)
        {
            if (i==0)
            {
               SpawnTile(1); 
            }
            else
            {
               SpawnTile(Random.Range(0, tilePrefabs.Length)); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z < SpawnPlace.z + 2f*(NumberOfTiles*tileLength))
        {
            SpawnTile(Random.Range(0,tilePrefabs.Length));
            SpawnTile(Random.Range(0,tilePrefabs.Length));
            if (countDestroy == 3)
            {
                countDestroy = 0;
                DestroyTile();
                DestroyTile();
            }
            else
            {
                countDestroy += 1;
            }
        }
    }
    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], SpawnPlace, transform.rotation);
        activeTiles.Add(go);
        SpawnPlace.z -= tileLength;
    }
    void DestroyTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
