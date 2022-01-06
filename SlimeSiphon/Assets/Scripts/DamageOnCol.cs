using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCol : MonoBehaviour
{
    public bool CanDamage = false;
    private bool IsOnPlayer = false;

    [SerializeField] private float Damage = 5f;
    private string TagTarget;

    void Start()
    {   
        IsOnPlayer = GetComponent<Health>().IsOnPlayer;
        CheckIfOnPlayer();
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag(TagTarget) && CanDamage)
        {
            ApplyDamage(col.gameObject);
        }
    }

    private void ApplyDamage(GameObject go)
    {
        go.GetComponent<Health>().Damage(Damage, gameObject);
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
