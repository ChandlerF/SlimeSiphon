using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public bool CanMove = true;

    private Rigidbody2D rb;

    private float horizontal, vertical;

    //Limits diagnol movement
    private float moveLimiter = 0.7f;

    [SerializeField] private float runSpeed = 20.0f;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
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

            if (newVel != Vector2.zero)     //Problem where this overrides the dash movement, make this a singleton, and so you can't move until it's done
            {
                rb.velocity = newVel;
            }
        }
    }
}
