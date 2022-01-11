using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWizard : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float StopDistance, RetreatDistance;
    private float MoveSpeed, StartShootTimer, ShootTimer;
    private bool Aggro = false, CanShoot = false;
    private Vector3 MoveDir;
    private Rigidbody2D rb;
    private Icicle IcicleGO;



    private void Start()
    {
        IcicleGO = GetComponent<Icicle>();
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


            //If too close
            if (direction.sqrMagnitude < RetreatDistance)
            {
                MoveDir = -direction.normalized;
                CanShoot = true;
            }
            else if (direction.sqrMagnitude > StopDistance)   //Too far away
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
                IcicleGO.Player = Player;
                IcicleGO.Ability();





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
        //ShootTimer = StartShootTimer;
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
