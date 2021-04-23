using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;    // To access the controller
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    public Animator animator;
    private Rigidbody2D rigidbody2d;
    private CircleCollider2D circlecollider2d;
    private PlayerCombat playerCombat;
    [SerializeField] private LayerMask foregroundlayermask;

    // dash
    public float dashDistance = 2f;
    public float dashCd = 5f;
    float nextDash = 0f;

    // Falling
    private float lastY;
    public float FallingThreshold = -0.01f;  //Adjust in inspector to appropriate value for the speed you want to trigger detecting a fall, probably by just testing (use negative numbers probably)
    float distancePerSecondSinceLastFrame;
    [HideInInspector] public bool isFalling = false;  //Other scripts can check this value to see if currently falling

    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        circlecollider2d = transform.GetComponent<CircleCollider2D>();
        playerCombat = GetComponent<PlayerCombat>();
        lastY = transform.position.y;
    }

    // Update is called once per frame
    public void Update()
    {
        //VELOCITY.Y IS THE
        //BEST AND MOST ACCURATE APPROACH FOR JUMPING AND FALLING
        if (rigidbody2d.velocity.y > 0)
        {
            animator.SetBool("Landed", false);
            animator.SetBool("IsJumping", true);
            animator.SetBool("IsFalling", false);
        }
        else if (rigidbody2d.velocity.y < 0)
        {
            animator.SetBool("Landed", false);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("Landed", true);
        }

        distancePerSecondSinceLastFrame = (transform.position.y - lastY) * Time.deltaTime;
        lastY = transform.position.y;  //set for next frame
        Falling();

        if (Time.time > nextDash)
        {
            if (controller.m_FacingRight && Input.GetKeyDown(KeyCode.Space))
            {
                if (CanMove(Vector3.right, dashDistance))
                {
                    Dash_right();
                    nextDash = Time.time + dashCd;
                }
            }
            if (!controller.m_FacingRight && Input.GetKeyDown(KeyCode.Space))
            {
                if (CanMove(Vector3.left, dashDistance))
                {
                    Dash_left();
                    nextDash = Time.time + dashCd;
                }
            }
        }

            // gets input for moving sideways
            // left(left arrow or "a") = -1; no input = 0; right(right arrow or "d") = 1
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Sets the animator parameter "Speed" to input's value
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Jumping
        if (IsGrounded() && Input.GetKeyDown(KeyCode.W) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit") && !playerCombat.staggered && !playerCombat.frozen)
        {
            float jumpVelocity = 20f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            animator.SetBool("Landed", false);
            animator.SetBool("IsJumping", true);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycasthit2d = Physics2D.CircleCast(circlecollider2d.bounds.center, 0.3797424f, Vector2.down, .1f, foregroundlayermask);
        return raycasthit2d.collider != null;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    bool CanMove(Vector3 dir, float distance)
    {
        return Physics2D.CircleCast(circlecollider2d.bounds.center, 0.3797424f, dir, distance, foregroundlayermask).collider == null;
    }

    public void Dash_right()
    {
        transform.position += Vector3.right * dashDistance;
    }

    public void Dash_left()
    {
        transform.position += Vector3.right * -dashDistance;
    }

    void Falling()
    {
        if (IsGrounded())
        {
            animator.SetBool("Landed", true);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
            return;
        }

        if (distancePerSecondSinceLastFrame < FallingThreshold)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }

        if (isFalling)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
            animator.SetBool("Landed", false);
        }
    }

    void FixedUpdate()
    {
        // Moveing player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }
}
