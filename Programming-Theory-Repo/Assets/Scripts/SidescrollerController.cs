using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerController : KinematicObject
{
    
    /// <summary>
    /// Max horizontal speed of the player.
    /// </summary>
    public float maxSpeed = 7;
    /// <summary>
    /// Initial jump velocity at the start of a jump.
    /// </summary>
    public float jumpTakeOffSpeed = 7;
    public float jumpDeceleration;
    
    public JumpState jumpState = JumpState.Grounded;
    private bool stopJump;
    /*internal new*/ public Collider2D collider2d;
    public bool controlEnabled = true;

    bool jump;
    Vector2 move;
    SpriteRenderer spriteRenderer;
    internal Animator animator;
    
    public Bounds Bounds => collider2d.bounds;
    /*
    public GameObject powerupIndicator;
    public GameObject chestOpen;
    public GameObject chestClosed;
    public ParticleSystem explosionParticle;
    public AudioClip openChestSound;
    private AudioSource playerAudio;
    public bool hasPowerup;

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chest") && hasPowerup)
        {
            chestClosed.SetActive(false);
            chestOpen.SetActive(true);
            Debug.Log("Collided with " + collision.gameObject.name + "with poweup set to " + hasPowerup);
            powerupIndicator.SetActive(false);
            explosionParticle.Play();
            playerAudio.PlayOneShot(openChestSound, 1.0f);
        }
    }
    */

    void Awake()
    {
        //playerAudio = GetComponent<AudioSource>();
        //hasPowerup = false;
        collider2d = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (controlEnabled)
        {
            move.x = Input.GetAxis("Horizontal");
            if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                jumpState = JumpState.PrepareToJump;
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = true;
            }
        }
        else
        {
            move.x = 0;
        }
        UpdateJumpState();
        base.Update();

        // powerupIndicator.transform.position = transform.position + new Vector3(0.3f, -0.1f, 0);
    }

    void UpdateJumpState()
    {
        jump = false;
        switch (jumpState)
        {
            case JumpState.PrepareToJump:
                jumpState = JumpState.Jumping;
                jump = true;
                stopJump = false;
                break;
            case JumpState.Jumping:
                if (!IsGrounded)
                {
                    jumpState = JumpState.InFlight;
                }
                break;
            case JumpState.InFlight:
                if (IsGrounded)
                {
                    jumpState = JumpState.Landed;
                }
                break;
            case JumpState.Landed:
                jumpState = JumpState.Grounded;
                break;
        }
    }

    protected override void ComputeVelocity()
    {
        if (jump && IsGrounded)
        {
            velocity.y = jumpTakeOffSpeed;
            jump = false;
        }
        else if (stopJump)
        {
            stopJump = false;
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * jumpDeceleration;
            }
        }

        if (move.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (move.x < -0.01f)
            spriteRenderer.flipX = true;

        animator.SetBool("grounded", IsGrounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    public enum JumpState
    {
        Grounded,
        PrepareToJump,
        Jumping,
        InFlight,
        Landed
    }
}