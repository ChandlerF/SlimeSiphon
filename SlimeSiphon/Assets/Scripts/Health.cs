using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashColor))]
public class Health : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 20f, CurrentHealth, KnockBackForce = 75f;

    public float StartMoveSpeed = 120f, MoveSpeed;

    private FlashColor FlashColorScript;
    private Rigidbody2D rb;

    private bool IsInvincible = false;

    public bool IsOnPlayer = false;

    public bool IsAlive = true;

    private GameObject PopupText;

    public MonoBehaviour MovementScript, AbilityScript;


    //Particle, and a color to set in inspector, so it looks like bits break off the person when damaged

    private void Awake()
    {
        MoveSpeed = StartMoveSpeed;
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        FlashColorScript = GetComponent<FlashColor>();
        rb = GetComponent<Rigidbody2D>();
        PopupText = Resources.Load("FloatingParent", typeof(GameObject)) as GameObject;
    }

    public void Heal(int hp)
    {
        if (IsAlive)
        {
            float dif = MaxHealth - CurrentHealth;

            if (hp > dif)
            {
                CurrentHealth += dif;
            }
            else
            {
                CurrentHealth += hp;
            }

            FlashColorScript.FlashGreen();
            SetSpeed();

            if (IsOnPlayer)
            {
                GetComponent<PlayerMovement>().MoveSpeed = MoveSpeed;
            }
        }
    }




    public void Damage(float dmg, Vector3 pos)
    {
        if (!IsInvincible && IsAlive)
        {
            CurrentHealth -= dmg;


            GameObject SpawnedText = Instantiate(PopupText, transform.position, Quaternion.identity);
            SpawnedText.GetComponent<PopupText>().Text.text = "-" + dmg.ToString();
            if (IsOnPlayer)
            {
                SpawnedText.GetComponent<PopupText>().Text.color = Color.red;
            }




            FlashColorScript.FlashRed();

            Knockback(pos, 1f);
            if (CurrentHealth <= 0)
            {
                Death();
                return;
            }

            SetSpeed();

            MovementScript.Invoke("TakenDamage", 0f);

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
        //Destroy(gameObject);

        IsAlive = false;
        GetComponent<Rigidbody2D>().mass *= 6;
        Destroy(MovementScript);
        AbilityScript.enabled = false;
        transform.tag = "Dead";
        gameObject.layer = 11;  //Dead
    }


    public void Knockback(Vector3 pos, float multiplier)
    {
        Vector3 direction = (Vector2)transform.position - (Vector2)pos;

        float distance = direction.magnitude;
        Vector3 Dir = direction / distance;

        rb.AddForce(Dir * (KnockBackForce * multiplier), ForceMode2D.Impulse);
    }


    private void MakeMortal()
    {
        IsInvincible = false;
    }


    private void SetSpeed()
    {
        float percent = CurrentHealth / MaxHealth;

        MoveSpeed = StartMoveSpeed * percent;
    }
}
