using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject FireBall;
    private bool IsOnPlayer = true;
    [SerializeField] private float Force;
    private Vector3 MoveDir;

    void Start()
    {
        IsOnPlayer = GetComponent<Health>().IsOnPlayer;
        MoveDir = -transform.right;
    }

    void Update()
    {
        
    }



    public void Ability()
    {
        //Get Dir   from Mouse      and         Velocity? or target the player directly

        //Quaternion newRotation = 
        GameObject SpawnedFB = Instantiate(FireBall, transform.position, Quaternion.identity);

        if (IsOnPlayer)
        {
            SpawnedFB.GetComponent<Health>().IsOnPlayer = IsOnPlayer;
            SpawnedFB.layer = 8;  //Player Layer
        }

        SpawnedFB.GetComponent<Rigidbody2D>().AddForce(MoveDir * Force, ForceMode2D.Impulse);
    }
}
