using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private bool afterJetpackPlaced;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!player.gameObject.GetComponent<PlayerController>().isJetpack)
        {
            if (player.gameObject.GetComponent<CharacterController>().isGrounded)
            {
                afterJetpackPlaced = true;
            }
            if (player.gameObject.GetComponent<CharacterController>().isGrounded)
            {
                transform.position = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
            }
            else
            {
                if (!afterJetpackPlaced)
                {
                transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
                }
                else
                {
                    transform.position = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);
                }
            }
        }
        else
        {
            afterJetpackPlaced = false;
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
        }
    }
}
