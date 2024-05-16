using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    private Vector3 playerDirection;
    private bool flyToPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flyToPlayer)
        {
            playerDirection = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector3(playerDirection.x, playerDirection.y + 0.5f, playerDirection.z)*50f*Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CoinMagnet")
        {
            player = GameObject.Find("Player");
            flyToPlayer = true;
        }
    }
}
