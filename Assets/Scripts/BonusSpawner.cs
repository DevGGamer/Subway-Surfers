using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public Transform[] points;
    public GameObject[] bonuses;
    // Start is called before the first frame update
    void Start()
    {
       int NumberOfSpawn = Random.Range(0, points.Length);
       for (int i=0; i<NumberOfSpawn; i++)
       {
            int NumberOfPoint = Random.Range(0, points.Length);
            if (points[NumberOfPoint].gameObject.GetComponent<PointPlaced>().isPlaced == false)
            {
                Instantiate(bonuses[Random.Range(0,bonuses.Length)], points[NumberOfPoint].position, Quaternion.identity);
            }
            else
            {
                NumberOfPoint = Random.Range(0, points.Length);
            }
       } 
    }
}
