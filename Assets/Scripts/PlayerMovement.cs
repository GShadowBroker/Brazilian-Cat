using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 40f;
    public PlayerController controller;
    public GameObject smokeEffect;

    // Audio
    public AudioSource jumpSound;
    public AudioSource dashSound;
    public AudioSource landingSound;

    private ParticleSystem smokeParticles;
    private GameObject instanceOfSmoke;
    private PlayerState player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool jumping;

    [System.Serializable]
    public class DashEvent : UnityEvent<bool> { };
    public DashEvent dashEvent;

    private float timeSinceJump = 0f;
    private float timeSinceDash = 1f;
    private float fallDeathThreshold = -3f;
    public float dashForce = 2000f;
    public float dashCooldownTime = 1f;

    private void Awake()
    {
        movement = new Vector2();

        if (dashEvent == null)
        {
            dashEvent = new DashEvent();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
        instanceOfSmoke = Instantiate(smokeEffect, new Vector3(transform.position.x, transform.position.y - 0.67f, -20f), Quaternion.identity);
        smokeParticles = instanceOfSmoke.GetComponent<ParticleSystem>();
        smokeParticles.Stop();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < fallDeathThreshold)
        {
            player.Die(false);
            return;
        }

        controller.Move(movement.x * Time.fixedDeltaTime, false, jumping);

        if (jumping) jumping = false;

        if (controller.m_Grounded && !animator.GetBool("Grounded") && timeSinceJump > 0.2f)
        {
            animator.SetBool("Grounded", true);
        }

        if (timeSinceDash > 0.2f)
        {
            animator.SetBool("Dash", false);
            dashEvent.Invoke(false);
        }

        timeSinceJump += Time.fixedDeltaTime;
        timeSinceDash += Time.fixedDeltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (player.dead) return;

        // set movement
        movement.x = context.ReadValue<Vector2>().x * movementSpeed;

        // play run animation
        animator.SetFloat("Speed", Mathf.Abs(context.ReadValue<Vector2>().x));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (player.dead) return;

        if (context.started && controller.m_Grounded && timeSinceJump > 0.2f)
        {
            jumping = true;
            timeSinceJump = 0f;
            animator.SetBool("Grounded", false);
            animator.SetTrigger("Jump");

            // audio
            jumpSound.Play();
        }

        else if (!context.performed)
        {
            // GO DOWN!
            if (rb.velocity.y > 0.1f) rb.AddForce(new Vector2(0f, -250f));
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {

        if (player.dead) return;

        if (context.started && timeSinceDash > dashCooldownTime)
        {
            // audio
            dashSound.Play();

            rb.AddForce(new Vector2(transform.localScale.x > 0 ? dashForce : -dashForce, 0f));
            timeSinceDash = 0f;

            // leave smoke
            instanceOfSmoke.transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, -20f);
            smokeParticles.Play();

            // play animation
            animator.SetBool("Dash", true);
            dashEvent.Invoke(true);
        }
    }

    public void OnTouchGround()
    {
        if (player.dead) return;

        // audio
        // landingSound.Play();

        // leave smoke
        instanceOfSmoke.transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, -20f);
        smokeParticles.Play();
    }
}
