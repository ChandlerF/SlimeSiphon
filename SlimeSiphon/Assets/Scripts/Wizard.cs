using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private GameObject Target;
    [SerializeField] private float MoveSpeed, StopDistance, RetreatDistance;
    [SerializeField] private float StartShootTimer;
    private float ShootTimer;
    private bool Aggro = false, CanShoot = false;
    private Vector3 MoveDir;
    private Rigidbody2D rb;
    private Fireball FireBall;



    private void Start()
    {
        FireBall = GetComponent<Fireball>();
        rb = GetComponent<Rigidbody2D>();

        ShootTimer = StartShootTimer;
    }


    void Update()
    {
        if (Aggro)
        {
            MoveDir = Vector3.zero;

            Vector3 direction = Target.transform.position - transform.position;


            //If too close
            if (direction.sqrMagnitude < RetreatDistance)
            {
                MoveDir = -direction.normalized;
                CanShoot = true;
            }
            else if(direction.sqrMagnitude > StopDistance)   //Too far away
            {
                //Moving Closer
                MoveDir = direction.normalized;
                CanShoot = false;
            }   
            else if (direction.sqrMagnitude <= StopDistance)
            {
                CanShoot = true;
            }




            if (ShootTimer <= 0 && CanShoot)
            {
                FireBall.Ability(Target.transform.position);

                ShootTimer = StartShootTimer;
            }
            else
            {
                ShootTimer -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Aggro)
        {
            rb.AddForce(MoveDir * MoveSpeed);
        }
    }


    public void TakenDamage()
    {
        ShootTimer = StartShootTimer;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Target = col.gameObject;
            Aggro = true;
        }
    }
}
