using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashRed))]
public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 20f, CurrentHealth, KnockBackForce = 50f;

    private FlashRed RedFlashScript;
    private Rigidbody2D rb;

    void Start()
    {
        CurrentHealth = MaxHealth;
        RedFlashScript = GetComponent<FlashRed>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Damage(float dmg, GameObject go)
    {
        CurrentHealth -= dmg;

        if(CurrentHealth <= 0)
        {
            Death();
        }

        RedFlashScript.Flash();

        KnockBack(go);
    }


    private void Death()
    {
        Destroy(gameObject);
    }


    private void KnockBack(GameObject go)
    {
        Vector3 MoveDir = transform.position - go.transform.position;

        rb.AddForce(MoveDir * KnockBackForce, ForceMode2D.Impulse);
    }
}
