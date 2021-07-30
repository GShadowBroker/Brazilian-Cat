using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private PlayerState player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null)
        {
            player.Die(false, false);
        }
    }
}
