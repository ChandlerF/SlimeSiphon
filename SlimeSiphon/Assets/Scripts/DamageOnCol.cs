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
            if(IsProjectile && col.transform.GetComponent<DamageOnCol>() != null && col.transform.GetComponent<DamageOnCol>().IsProjectile)
            {
                Destroy(col.gameObject);
                AudioManager.instance.Play("Explosion");
            }

            ApplyDamage(col.gameObject);

            if (IsProjectile)
            {
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
