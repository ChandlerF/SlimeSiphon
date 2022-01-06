using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    private bool IsOnPlayer = true;
    [SerializeField] private float Force;
    //private Vector3 MoveDir;

    void Start()
    {
        IsOnPlayer = GetComponent<Health>().IsOnPlayer;
       // MoveDir = -transform.right;
    }

    void Update()
    {
        
    }



    public void Ability(Vector3 Target)
    {
        Vector3 direction = Target - transform.position;
        Vector3 Dir = direction.normalized;
        //Get Dir   from Mouse      and         Velocity? or target the player directly

        //Quaternion newRotation = 
        GameObject SpawnedFB = Instantiate(FireBall, transform.position, Quaternion.identity);

        if (IsOnPlayer)
        {
            SpawnedFB.GetComponent<Health>().IsOnPlayer = IsOnPlayer;
            SpawnedFB.layer = 8;  //Player Layer
        }

        SpawnedFB.transform.rotation = Quaternion.LookRotation(Vector3.forward, -Dir);

        SpawnedFB.GetComponent<Rigidbody2D>().AddForce(Dir * Force, ForceMode2D.Impulse);
    }
}
