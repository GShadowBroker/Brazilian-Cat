using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private PlayerState player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null && collision.contacts[0].normal.y < -0.5)
        {
            player.Die(true);
        }
    }
}
