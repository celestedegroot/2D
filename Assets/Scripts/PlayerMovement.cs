using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 6f;
    private float jumpPower = 24f;
    private float climbSpeed = 3f;
    private float dashPower = 4f;
    private bool isFacingRight = true;
    private bool isDashing = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (IsGrounded())
        {
            isDashing = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.8f);
        }

        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }

        if (Input.GetButton("Climb") && Physics2D.OverlapBox(rb.position, new Vector2(1.2f, 0.5f), 0, wallLayer))
        {
            Climb();
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 1f;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            return true;
        }

        else if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, wallLayer))
        {
            return true;
        }

        return false;
    }

    private void Dash()
    {
        if (isDashing == false)
        {
            isDashing = true;

            if (horizontal != 0f)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(horizontal, 0f), dashPower, wallLayer | groundLayer);
                if (hit.collider != null)
                {
                    if (Physics2D.OverlapCircle(new Vector2(hit.point.x + (horizontal * dashPower), hit.point.y), 0.1f, wallLayer | groundLayer)) rb.position = new Vector2(hit.point.x - (horizontal * 0.5f), rb.position.y);
                    else rb.position = new Vector2(hit.point.x + (horizontal * dashPower), rb.position.y);
                }

                else rb.position = new Vector2(rb.position.x + horizontal * dashPower, rb.position.y);
            }

            else if (isFacingRight)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(1f, 0f), dashPower, wallLayer | groundLayer);
                if (hit.collider != null)
                {
                    if (Physics2D.OverlapCircle(new Vector2(hit.point.x + dashPower, hit.point.y), 0.1f, wallLayer | groundLayer)) rb.position = new Vector2(hit.point.x - 0.5f, rb.position.y);
                    else rb.position = new Vector2(hit.point.x + dashPower, rb.position.y);
                }

                else rb.position = new Vector2(rb.position.x + dashPower, rb.position.y);
            }

            else
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(-1f, 0f), dashPower, wallLayer | groundLayer);
                if (hit.collider != null)
                {
                    if (Physics2D.OverlapCircle(new Vector2(hit.point.x - dashPower, hit.point.y), 0.1f, wallLayer | groundLayer)) rb.position = new Vector2(hit.point.x + 0.5f, rb.position.y);
                    else rb.position = new Vector2(hit.point.x - dashPower, rb.position.y);
                }

                else rb.position = new Vector2(rb.position.x - dashPower, rb.position.y);
            }
        }
    }

    private void Climb()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(0f, 0f);
        rb.position = new Vector2(rb.position.x, rb.position.y + vertical * climbSpeed * Time.deltaTime);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
