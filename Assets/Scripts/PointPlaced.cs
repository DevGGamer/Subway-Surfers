using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPlaced : MonoBehaviour
{
    public bool isPlaced = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin" || other.gameObject.tag == "Magnet")
        {
            isPlaced = true;
        }
    }
}
