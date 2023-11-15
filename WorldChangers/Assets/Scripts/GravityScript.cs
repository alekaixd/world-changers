using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool hasFlippedDown;
    private bool hasFlippedUp;
    void Start()
    {
        Flip();
    }

    void Update()
    {
        if(gameObject.transform.position.y < 0)
        {
            rb.gravityScale = -1;
            if (!hasFlippedDown)
            {
                hasFlippedUp = false;
                hasFlippedDown = true;
                Flip();
            }
        }
        else
        {
            rb.gravityScale = 1;
            if (!hasFlippedUp)
            {
                hasFlippedDown = false;
                hasFlippedUp = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        int noRotation = 0;
        int flippedRotation = 180;
        if (hasFlippedUp)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, noRotation);
        }
        else if (hasFlippedDown)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, flippedRotation);
        }
    }
}
