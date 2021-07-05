using UnityEngine;
using Pathfinding;

public class CorujaAI : MonoBehaviour
{
    public float aggroRadius = 10f;
    private AIPath aiPath;
    private PlayerState player;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
    }

    private void Start()
    {
        aiPath.canSearch = false;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, aiPath.destination) <= aggroRadius)
        {
            aiPath.canSearch = true;
        }
        else
        {
            aiPath.canSearch = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null)
        {
            player.Die(true);
        }
    }
}
