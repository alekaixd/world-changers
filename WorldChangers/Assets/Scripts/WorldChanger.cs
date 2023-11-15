using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public Vector2 target = new Vector2(5, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            target.x = target.x * -1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(target.y == 0)
            {
                target.y = -5;
            }
            else
            {
                target.y = 0;
            }
        }


        transform.position = Vector2.MoveTowards(transform.position, target, speed);
    }
}
