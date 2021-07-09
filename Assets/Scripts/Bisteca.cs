using UnityEngine;

public class Bisteca : MonoBehaviour
{
    public float movementRadius = 0.5f;
    public float deadZone = 0.1f;
    public float smoothing = 0.1f;
    private bool goingUp;
    private Vector3 startingPos;
    private Vector3 upperLimit;
    private Vector3 bottomLimit;
    private PlayerState player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        startingPos = transform.position;
        upperLimit = new Vector3(transform.position.x, transform.position.y + movementRadius);
        bottomLimit = new Vector3(transform.position.x, transform.position.y - movementRadius);
    }

    private void Update()
    {
        if (goingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, upperLimit, smoothing);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomLimit, smoothing);
        }


        if (transform.position.y >= upperLimit.y - deadZone) goingUp = false;
        else if (transform.position.y <= bottomLimit.y + deadZone) goingUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>() != null)
        {
            player.IncrementScore();
            Destroy(gameObject);
        }
    }
}
