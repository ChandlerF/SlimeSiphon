using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private float MoveSpeed = 140f;
    private Rigidbody2D rb;
    private Vector3 MoveDir;
    [SerializeField] private float AttackRange = 20f;
    private Charge ChargeScript;
    public bool CanMove = true;

    private enum State
    {
        Idle,
        Aggro,
        Retreat,
    }

    private State state = State.Idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        ChargeScript = GetComponent<Charge>();

        MoveSpeed = GetComponent<Health>().MoveSpeed;
    }

    private void Update()
    {
        if (state == State.Aggro)
        {
            MoveDir = Vector3.zero;

            Vector3 direction = Player.transform.position - transform.position;

            MoveDir = direction.normalized;


            if (direction.sqrMagnitude < AttackRange)
            {
                //Attack
                ChargeScript.Ability();

                //Set state to retreat, after seconds
                Invoke("Retreat", 1.2f);
            }
        }
        else if(state == State.Retreat)
        {
            MoveDir = Vector3.zero;

            Vector3 direction = Player.transform.position - transform.position;



            if (direction.sqrMagnitude < AttackRange * 1.5f)
            {
                MoveDir = -direction.normalized;
            }
            else
            {
                state = State.Aggro;
            }
        }
    }
    void FixedUpdate()
    {
        if (CanMove)
        {
            if (state == State.Aggro || state == State.Retreat)
            {
                rb.AddForce(MoveDir * MoveSpeed);
            }
        }
    }

    public void TakenDamage()
    {
        if(state != State.Aggro)
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            state = State.Aggro;
        }

        MoveSpeed = GetComponent<Health>().MoveSpeed;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player = col.gameObject;

            ChargeScript.Player = Player;

            state = State.Aggro;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (state == State.Aggro && col.CompareTag("Enemy"))
        {
            col.GetComponent<Health>().BecomeAggro();
        }
    }
    public void BecomeAggro()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        state = State.Aggro;
    }

    private void Retreat()
    {
        state = State.Retreat;
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
