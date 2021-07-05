using UnityEngine;
using Pathfinding;

public class CorujaGFX : MonoBehaviour
{
    private AIPath aiPath;
    private float scale;

    private void Awake()
    {
        aiPath = GetComponentInParent<AIPath>();
    }

    private void Start()
    {
        scale = transform.localScale.x;
    }

    private void Update()
    {
        if (aiPath.desiredVelocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(scale, scale, scale);
        }
        else if (aiPath.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-scale, scale, scale);
        }
    }
}
