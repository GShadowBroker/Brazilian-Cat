using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerState : MonoBehaviour
{
    public CinemachineVirtualCamera vcam1;
    public CinemachineVirtualCamera vcam2;

    public Vector2 dyingJumpForce = new Vector2(250f, 500f);
    private Animator screenAnimator;
    private Animator playerAnimator;
    private Rigidbody2D rb;
    private CircleCollider2D collision;
    private PlayerMovement playerMovement;
    private bool fadingOut = false;
    private float fadingTimer = 0f;
    public float fadingDuration = 2f;
    private bool zoomingIn = false;
    public bool dead;

    void Start()
    {
        screenAnimator = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<CircleCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // zoom in every frame
        if (zoomingIn)
        {
            vcam2.m_Lens.OrthographicSize = Mathf.Lerp(vcam1.m_Lens.OrthographicSize, 1f, 6f * Time.deltaTime);
        }

        // normalized time (0-1) represents the %. If greater than 1, animation finished.
        if (fadingOut && fadingTimer >= fadingDuration)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (fadingOut) fadingTimer += Time.deltaTime;
    }

    public void Die(bool playScaredAnimation)
    {
        dead = true;
        // switch camera
        vcam1.Priority = 0;
        vcam2.Priority = 10;

        // remove follow behavior
        vcam1.Follow = null;
        vcam2.Follow = null;

        playerMovement.enabled = false;
        collision.enabled = false;

        if (playScaredAnimation)
        {
            playerAnimator.SetBool("Scared", true);

            rb.gravityScale = rb.gravityScale / 2f;
            rb.AddForce(dyingJumpForce);
        }

        screenAnimator.SetTrigger("FadeOut");
        fadingOut = true;
    }
}
