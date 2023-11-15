using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCurves : MonoBehaviour
{
    private float horizontal;
    private float speed;
    public float jumpingPower = 10f;
    private bool isFacingRight = true;
    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public AnimationCurve movementCurve;
    public AnimationCurve decelerationCurve;
    private float decelerationTime;
    private float accelerationTime;
    private int jumpDirection;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.075f;
    private float jumpBufferCounter;


    void Update()
    {
        if (gameObject.transform.position.y > 0)
        {
            jumpDirection = 1;
        }
        else
        {
            jumpDirection = -1;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpDirection * jumpingPower);
            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0f && jumpDirection == 1)
        {
            if (jumpDirection == 1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }

        if (Input.GetButtonUp("Jump") && rb2d.velocity.y < 0f && jumpDirection == -1)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetButton("Horizontal"))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            decelerationTime = 0;
            speed = movementCurve.Evaluate(accelerationTime);
            accelerationTime += Time.deltaTime;
        }

        if (Input.GetButton("Horizontal") == false)
        {
            accelerationTime = 0;
            speed = decelerationCurve.Evaluate(decelerationTime);
            decelerationTime += Time.deltaTime;
        }

        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            accelerationTime = 0;
        }
        Flip();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.01f, LayerMask.GetMask("Ground"));
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
