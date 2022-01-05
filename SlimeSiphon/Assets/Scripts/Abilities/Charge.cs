using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charge : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float DashForce = 80f;


    private Vector3 MoveDir;

    private bool CanCharge = true;

    [SerializeField] private bool IsOnPlayer = true;

/*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, rb.velocity.normalized * 10);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDir * 5);
    }
*/


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanCharge)
        {
            MoveDir = rb.velocity.normalized;


            if (MoveDir == Vector3.zero)
            {
                MoveDir = transform.right;
            }

            ChargeFunc();
        }
    }


    private void ChargeFunc()
    {
        CanCharge = false;

        if (IsOnPlayer)
        {
            PlayerMovement.instance.CanMove = false;
        }

        FreezePos();

        //Set material or whatever to be white, then change it to normal in unfreeze or after the dash
    }





    private void FreezePos()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        Invoke("UnFreezePos", 1f);
    }

    private void UnFreezePos()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Dash();
    }

    private void Dash()
    {
        rb.AddForce(MoveDir * DashForce, ForceMode2D.Impulse);

        Invoke("EnablePlayerMovement", 0.4f);
    }

    private void EnablePlayerMovement()
    {
        if (IsOnPlayer)
        {
            PlayerMovement.instance.CanMove = true;
        }

        CanCharge = true;
    }
}
