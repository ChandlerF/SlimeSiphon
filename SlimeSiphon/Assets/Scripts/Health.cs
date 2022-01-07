using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashRed))]
public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 20f, CurrentHealth, KnockBackForce = 75f;

    private FlashRed RedFlashScript;
    private Rigidbody2D rb;

    private bool IsInvincible = false;

    public bool IsOnPlayer = false;

    public MonoBehaviour Script;

    //Particle, and a color to set in inspector, so it looks like bits break off the person when damaged

    void Start()
    {
        CurrentHealth = MaxHealth;
        RedFlashScript = GetComponent<FlashRed>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void Damage(float dmg, GameObject go)
    {
        if (!IsInvincible)
        {
            CurrentHealth -= dmg;

            if (CurrentHealth <= 0)
            {
                Death();
                return;
            }

            Script.Invoke("TakenDamage", 0f);

            RedFlashScript.Flash();

            KnockBack(go);

            IsInvincible = true;
            Invoke("MakeMortal", 0.4f);

            /*
            if (IsOnPlayer)     //TakenDamage() is on every Player/Enemy
            {
                //ScreenShake for being damaged
            }
            else
            {
                //for doing damage
            }*/
        }
    }


    public void Death()
    {
        //Particles
        //ScreenShake
        Destroy(gameObject);
    }


    private void KnockBack(GameObject go)
    {
        Vector3 MoveDir = transform.position - go.transform.position;

        rb.AddForce(MoveDir * KnockBackForce, ForceMode2D.Impulse);
    }


    private void MakeMortal()
    {
        IsInvincible = false;
    }
}
