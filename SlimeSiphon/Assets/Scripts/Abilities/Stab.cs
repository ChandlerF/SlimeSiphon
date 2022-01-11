using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    private GameObject StabVisual;
    private bool IsOnPlayer = true;
    [SerializeField] private float Force = 20;
    public Vector3 direction, mousePos;
    private Health HealthScript;
    public GameObject Player;


    void Start()
    {
        HealthScript = GetComponent<Health>();

        IsOnPlayer = HealthScript.IsOnPlayer;

        StabVisual = Resources.Load("Stab", typeof(GameObject)) as GameObject;
    }

    public void Ability()
    {

        if (IsOnPlayer)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (Vector2)mousePos - (Vector2)transform.position;

            HealthScript.Knockback(mousePos, 0.5f);
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
        GameObject SpawnedStab = Instantiate(StabVisual, transform.position, Quaternion.identity);

        if (IsOnPlayer)
        {
            SpawnedStab.GetComponent<Health>().IsOnPlayer = IsOnPlayer;
            SpawnedStab.layer = 8;  //Player Layer
            SpawnedStab.tag = "PlayerProjectiles";
        }

        SpawnedStab.transform.rotation = Quaternion.LookRotation(Vector3.forward, -Dir);

        AudioManager.instance.Play("Stab");
        SpawnedStab.GetComponent<Rigidbody2D>().AddForce(Dir * Force, ForceMode2D.Impulse);
    }
}
