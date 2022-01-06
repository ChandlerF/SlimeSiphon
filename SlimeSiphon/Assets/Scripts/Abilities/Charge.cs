using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageOnCol), typeof(FlashWhite))]
public class Charge : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float DashForce = 250f;


    private Vector3 MoveDir;

    private bool CanCharge = true;

    private bool IsOnPlayer = true;

    [SerializeField] private FlashWhite FlashScript;

    [SerializeField] private float WaitSecToDash = 0.3f;

    private DamageOnCol ColScript;

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

        FlashScript = GetComponent<FlashWhite>();

        ColScript = GetComponent<DamageOnCol>();

        IsOnPlayer = GetComponent<Health>().IsOnPlayer;
    }



    public void Ability()
    {
        if (CanCharge)
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
        FlashScript.Flash();

        ColScript.CanDamage = true;
    }

    private void UnFreezePos()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        transform.rotation = Quaternion.identity;

        Dash();
    }

    private void Dash()
    {
        rb.AddForce(MoveDir * DashForce, ForceMode2D.Impulse);

        Invoke("EnablePlayerMovement", WaitSecToDash);
    }

    private void EnablePlayerMovement()
    {
        if (IsOnPlayer)
        {
            PlayerMovement.instance.CanMove = true;
        }

        ColScript.CanDamage = false;
        CanCharge = true;
    }
}
