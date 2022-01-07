using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageOnCol), typeof(FlashColor))]
public class Charge : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float DashForce = 250f;


    private Vector3 MoveDir, direction;

    private bool CanCharge = true;

    private bool IsOnPlayer = true;

    [SerializeField] private FlashColor FlashColorScript;

    [SerializeField] private float WaitSecToDash = 0.3f;

    private DamageOnCol ColScript;

    public GameObject Player;

    private Health HealthScript;

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

        FlashColorScript = GetComponent<FlashColor>();

        ColScript = GetComponent<DamageOnCol>();

        HealthScript = GetComponent<Health>();

        IsOnPlayer = HealthScript.IsOnPlayer;
    }



    public void Ability()
    {
        if (CanCharge)
        {
            if (IsOnPlayer)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (Vector2)mousePos - (Vector2)transform.position;
            }
            else
            {
                direction = (Vector2)Player.transform.position - (Vector2)transform.position;
            }

            float distance = direction.magnitude;
            MoveDir = direction / distance;

            ChargeFunc();
        }
    }

    private void ChargeFunc()
    {
        HealthScript.MovementScript.Invoke("StopMovement", 0f);

        CanCharge = false;

        FreezePos();

        //Set material or whatever to be white, then change it to normal in unfreeze or after the dash
    }





    private void FreezePos()
    {
        Invoke("UnFreezePos", 0.5f);
        FlashColorScript.FlashWhite();
    }

    private void UnFreezePos()
    {
        ColScript.CanDamage = true;

        Dash();
    }

    private void Dash()
    {
        rb.AddForce(MoveDir * DashForce, ForceMode2D.Impulse);

        Invoke("EnablePlayerMovement", WaitSecToDash);
    }

    private void EnablePlayerMovement()
    {
        ColScript.CanDamage = false;

        if (HealthScript.IsAlive)
        {
            HealthScript.MovementScript.Invoke("AllowMovement", 0f);
            CanCharge = true;
        }
    }
}

























































/*using System.Collections;
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
    


void Start()
{
    rb = GetComponent<Rigidbody2D>();

    FlashScript = GetComponent<FlashWhite>();

    ColScript = GetComponent<DamageOnCol>();

    IsOnPlayer = GetComponent<Health>().IsOnPlayer;
}



public void Ability(GameObject target)
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
    rb.constraints = RigidbodyConstraints2D.FreezeAll;
    Invoke("UnFreezePos", 1f);
    FlashScript.Flash();
}

private void UnFreezePos()
{
    ColScript.CanDamage = true;

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
*/