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

    

    void Update()
    {
        
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
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

        Flip();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
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
