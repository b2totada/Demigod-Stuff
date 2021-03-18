using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // To access the controller
    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 40f;

    bool jump = false;

    public Animator animator;

    private Rigidbody2D rigidbody2d;

    private CircleCollider2D circlecollider2d;

    float dashDistance = 2f;

    [SerializeField] private LayerMask foregroundlayermask;

    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        circlecollider2d = transform.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (controller.m_FacingRight)
        {
            if (CanMove(Vector3.right, dashDistance))
            {
                Dash();
            }
        }
        if (!controller.m_FacingRight)
        {
            if (CanMove(Vector3.left, dashDistance))
            {
                Dash();
            }
        }

        // gets input for moving sideways
        // left(left arrow or "a") = -1; no input = 0; right(right arrow or "d") = 1
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Sets the animator parameter "Speed" to input's value
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Jumping
        if (IsGrounded() && ((Input.GetKeyDown(KeyCode.W))))
        {
            float jumpVelocity = 20f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;

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

    public void Dash()
    {
        if (controller.m_FacingRight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += Vector3.right * dashDistance;
            }
        }

        if (!controller.m_FacingRight)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += Vector3.right * -dashDistance;
            }
        }
    }

    void FixedUpdate()
    {
        // Moveing player
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
