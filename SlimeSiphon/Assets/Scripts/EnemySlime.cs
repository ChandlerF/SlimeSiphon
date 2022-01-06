using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    private bool Aggro = false;
    private GameObject Player;
    [SerializeField] private float MoveSpeed = 50f;
    private Rigidbody2D rb;
    private Vector3 MoveDir;
    [SerializeField] private float AttackRange = 20f;
    private Charge ChargeScript;

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
            }
        }
    }
    void FixedUpdate()
    {
        if (state == State.Aggro)
        {
            //Chase
            //If in range, attack
            //Then retreat
            //Repeat

            //transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, MoveSpeed);

            rb.AddForce(MoveDir * MoveSpeed);
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player = col.gameObject;

            Aggro = true;
        }
    }
}
