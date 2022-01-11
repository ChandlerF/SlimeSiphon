using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject FireBall;
    private bool IsOnPlayer = true;
    [SerializeField] private float Force = 20;
    public Vector3 direction, mousePos;
    private Health HealthScript;
    public GameObject Player;


    void Start()
    {
        HealthScript = GetComponent<Health>();

        IsOnPlayer = HealthScript.IsOnPlayer;

        FireBall = Resources.Load("FireBall", typeof(GameObject)) as GameObject;
    }

    public void Ability()
    {

        if (IsOnPlayer)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (Vector2)mousePos - (Vector2)transform.position;

            //GetComponent<PlayerMovement>().StopMovement();
            HealthScript.Knockback(mousePos, 0.5f);
            //GetComponent<PlayerMovement>().Invoke("AllowMovement", 0.2f);
        }
        else
        {
            direction = (Vector2)Player.transform.position - (Vector2)transform.position;


            HealthScript.Knockback(Player.transform.position, 0.5f);
        }
        float distance = direction.magnitude;
        Vector3 Dir = direction / distance;

        //Get Dir   from Mouse      and         Velocity? or target the player directly

        //Quaternion newRotation = 
        GameObject SpawnedFB = Instantiate(FireBall, transform.position, Quaternion.identity);

        if (IsOnPlayer)
        {
            SpawnedFB.GetComponent<Health>().IsOnPlayer = true;
            SpawnedFB.layer = 8;  //Player Projectiles Layer
            SpawnedFB.tag = "PlayerProjectiles";
        }
        else
        {
            SpawnedFB.GetComponent<Health>().IsOnPlayer = false;
            SpawnedFB.layer = 9;  //Enemy Projectiles Layer
            SpawnedFB.tag = "EnemyProjectiles";
        }

        SpawnedFB.transform.rotation = Quaternion.LookRotation(Vector3.forward, -Dir);

        AudioManager.instance.Play("ShootFireball");
        SpawnedFB.GetComponent<Rigidbody2D>().AddForce(Dir * Force, ForceMode2D.Impulse);
    }
}
