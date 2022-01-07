using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public static PlayerMovement instance;

    public bool CanMove = true;

    private Rigidbody2D rb;

    private float horizontal, vertical;

    //Limits diagnol movement
    private float moveLimiter = 0.7f;

    private float runSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        runSpeed = GetComponent<Health>().MoveSpeed;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (CanMove)
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            Vector2 newVel = new Vector2(horizontal * runSpeed, vertical * runSpeed);

            if (newVel != Vector2.zero)
            {
                //rb.velocity = newVel;
                rb.AddForce(newVel);
            }
        }
    }


    public void TakenDamage()
    {
        //Screenshake
        //Freeze Frame
        //Sound FX

         runSpeed = GetComponent<Health>().MoveSpeed;
    }

    public void StopMovement()
    {
        CanMove = false;
    }
    public void AllowMovement()
    {
        CanMove = true;
    }
}
