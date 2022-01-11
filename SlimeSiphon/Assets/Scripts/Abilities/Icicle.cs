using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    private GameObject IcicleGO;
    private bool IsOnPlayer = true;
    [SerializeField] private float Force = 20;
    public Vector3 direction, mousePos;
    private Health HealthScript;
    public GameObject Player;


    void Start()
    {
        HealthScript = GetComponent<Health>();

        IsOnPlayer = HealthScript.IsOnPlayer;

        IcicleGO = Resources.Load("Icicle", typeof(GameObject)) as GameObject;
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
        GameObject SpawnedIcicle = Instantiate(IcicleGO, transform.position, Quaternion.identity);

        if (IsOnPlayer)
        {
            SpawnedIcicle.GetComponent<Health>().IsOnPlayer = true;
            SpawnedIcicle.layer = 8;  //Player Projectiles Layer
            SpawnedIcicle.tag = "PlayerProjectiles";
        }
        else
        {
            SpawnedIcicle.GetComponent<Health>().IsOnPlayer = false;
            SpawnedIcicle.layer = 9;  //Enemy Projectiles Layer
            SpawnedIcicle.tag = "EnemyProjectiles";
        }

        SpawnedIcicle.transform.rotation = Quaternion.LookRotation(Vector3.forward, -Dir);

        AudioManager.instance.Play("ShootIcicle");
        SpawnedIcicle.GetComponent<Rigidbody2D>().AddForce(Dir * Force, ForceMode2D.Impulse);
    }
}
