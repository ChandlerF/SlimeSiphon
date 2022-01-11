using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Comes with Charge Script, allows gameObject to hurt whatever it touches when enabled
public class DamageOnCol : MonoBehaviour
{
    public bool CanDamage = false;
    private bool IsOnPlayer = false;

    [SerializeField] private float Damage = 10f;
    private string TagTarget;
    [SerializeField] private bool KillOnCol = false;
    public bool IsProjectile = false;

    void Start()
    {   
        IsOnPlayer = GetComponent<Health>().IsOnPlayer;
        CheckIfOnPlayer();
    }



    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag(TagTarget) && CanDamage)
        {
            ApplyDamage(col.gameObject);

            if (KillOnCol)
            {
                Destroy(gameObject);
            }
        }
        else if(IsProjectile && col.transform.CompareTag(TagTarget + "Projectiles"))
        {
            Destroy(col.gameObject);
            AudioManager.instance.Play("Explosion");
            Destroy(gameObject);
        }
        else if (IsProjectile && col.transform.CompareTag("Environment"))
        {
            AudioManager.instance.Play("HitWall");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsProjectile && !col.isTrigger || IsProjectile && col.transform.CompareTag(TagTarget + "Projectiles"))
        {
            if (col.transform.CompareTag(TagTarget) && CanDamage)
            {
                ApplyDamage(col.gameObject);

            }
            else if (col.transform.CompareTag(TagTarget + "Projectiles"))
            {
                Destroy(col.gameObject);
                AudioManager.instance.Play("Explosion");
                Destroy(gameObject);
            }
        }
    }

    private void ApplyDamage(GameObject go)
    {
        go.GetComponent<Health>().Damage(Damage, gameObject.transform.position);
    }



    private void CheckIfOnPlayer()
    {
        if (IsOnPlayer)
        {
            TagTarget = "Enemy";
        }
        else
        {
            TagTarget = "Player";
        }
    }
}
