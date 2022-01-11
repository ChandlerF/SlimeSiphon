using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Vector2 TimerRange;
    private float Timer;

    private GameObject Fireball;

    void Start()
    { 
        Timer = Random.Range(TimerRange.x, TimerRange.y);
        Fireball = Resources.Load("Fireball", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Shoot();
            Timer = Random.Range(TimerRange.x, TimerRange.y);
            
        }
    }



    private void Shoot()
    {
        GameObject SpawnedFB = Instantiate(Fireball, transform.position, Quaternion.identity);

        SpawnedFB.GetComponent<Health>().IsOnPlayer = false;
        SpawnedFB.layer = 9;  //Enemy Projectiles Layer
        SpawnedFB.tag = "EnemyProjectiles";


        SpawnedFB.transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.up);

        AudioManager.instance.Play("ShootFireball");
        SpawnedFB.GetComponent<Rigidbody2D>().AddForce(-transform.up * 20f, ForceMode2D.Impulse);
    }
}
