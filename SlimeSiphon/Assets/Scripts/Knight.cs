using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float StopDistance;
    private float MoveSpeed, StartShootTimer, ShootTimer;
    private bool Aggro = false, CanShoot = false;
    private Vector3 MoveDir;
    private Rigidbody2D rb;
    private Stab Stab;



    private void Start()
    {
        Stab = GetComponent<Stab>();
        rb = GetComponent<Rigidbody2D>();


        StartShootTimer = GetComponent<Health>().Delay;
        ShootTimer = StartShootTimer;
        MoveSpeed = GetComponent<Health>().MoveSpeed;
    }


    void Update()
    {
        if (Aggro)
        {
            MoveDir = Vector3.zero;

            Vector3 direction = Player.transform.position - transform.position;


            if (direction.sqrMagnitude > StopDistance)   //Too far away
            {
                //Moving Closer
                MoveDir = direction.normalized;
                CanShoot = false;
            }
            else
            {
                CanShoot = true;
            }




            if (ShootTimer <= 0 && CanShoot)
            {
                Stab.Player = Player;
                StabAbility();
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

    private void StabAbility()
    {
        Stab.Ability();

        ShootTimer = StartShootTimer;
    }

    public void TakenDamage()
    {
        ShootTimer = StartShootTimer;
        if (!Aggro)
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            Aggro = true;
        }

        MoveSpeed = GetComponent<Health>().MoveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player = col.gameObject;
            Aggro = true;
        }
        else if (col.CompareTag("PlayerProjectiles"))
        {
            Stab.Player = col.gameObject;
            StabAbility();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (Aggro == true && col.CompareTag("Enemy"))
        {
            col.GetComponent<Health>().BecomeAggro();
        }
    }
    public void BecomeAggro()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Aggro = true;
    }
}
