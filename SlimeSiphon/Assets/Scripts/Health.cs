using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashColor))]
public class Health : MonoBehaviour
{
    public float MaxHealth = 20f, CurrentHealth;
    [SerializeField] private float KnockBackForce = 75f;

    public float StartMoveSpeed = 120f, MoveSpeed;

    private FlashColor FlashColorScript;
    private Rigidbody2D rb;

    [SerializeField] private bool IsInvincible = false;

    public bool IsOnPlayer = false;

    public bool IsAlive = true;

    private GameObject PopupText;

    public MonoBehaviour MovementScript, AbilityScript;

    public float Delay;

    private Color particleColor;

    private GameObject HitParticles;

    private SpriteRenderer sr;

    [SerializeField] private Sprite DeadSprite;
    private string OriginalTag;
    private int OriginalLayer;
    private float OriginalMass;
    private Sprite OriginalSprite;


    private void Awake()
    {
        MoveSpeed = StartMoveSpeed;
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        FlashColorScript = GetComponent<FlashColor>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        PopupText = Resources.Load("FloatingParent", typeof(GameObject)) as GameObject;
        HitParticles = Resources.Load("HitParticles", typeof(GameObject)) as GameObject;


        if (IsOnPlayer)
        {
            particleColor = new Color(99f / 255f, 155f / 255f, 255f / 255f, 1); //639BFF
        }
        else
        {
            particleColor = sr.color;
        }


        OriginalTag = transform.tag;
        OriginalLayer = gameObject.layer;
        OriginalSprite = sr.sprite;
        OriginalMass = rb.mass;
    }

    public void Heal(float hp)
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
                MovementScript.Invoke("Healed", 0f);
            }

            GameObject SpawnedText = Instantiate(PopupText, transform.position, Quaternion.identity);
            SpawnedText.GetComponent<PopupText>().Text.text = "+" + hp.ToString();
            SpawnedText.GetComponent<PopupText>().Text.color = Color.green;
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

            GameObject SpawnedParticles = Instantiate(HitParticles, transform.position, Quaternion.identity);


            SpawnedParticles.GetComponent<ParticleSystem>().startColor = particleColor;

            MovementScript.Invoke("TakenDamage", 0f);

            if (CurrentHealth <= 0)
            {
                Death();
                return;
            }

            SetSpeed();


            IsInvincible = true;
            Invoke("MakeMortal", 0.5f);

            
            if (IsOnPlayer)
            {
                CameraShake.cam.Trauma += 0.35f;
                AudioManager.instance.Play("HitPlayer");
                FreezeFrame(0.1f);
            }
            else
            {
                CameraShake.cam.Trauma += 0.12f;
                AudioManager.instance.Play("HitEnemy");
                FreezeFrame(0.05f);
            }
        }
    }


    public void Death()
    {
        IsAlive = false;
        GetComponent<Rigidbody2D>().mass *= 6;

        sr.color = new Color(sr.color.r - 0.3f, sr.color.g - 0.3f, sr.color.b - 0.3f);

        transform.tag = "Dead";
        gameObject.layer = 11;  //"Dead" Layer

        MovementScript.enabled = false;

        if (IsOnPlayer)
        {
            AudioManager.instance.Play("PlayerDeath");
        }
        else
        {
            AudioManager.instance.Play("EnemyDeath");
            AbilityScript.enabled = false;
            sr.sprite = DeadSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

        Door.instance.UpdateCount();

        CameraShake.cam.Trauma += 0.2f;
        FreezeFrame(0.1f);
    }

    public void Revive()
    {
        IsAlive = true;
        GetComponent<Rigidbody2D>().mass = OriginalMass;

        sr.color = new Color(sr.color.r + 0.3f, sr.color.g + 0.3f, sr.color.b + 0.3f);

        transform.tag = OriginalTag;
        gameObject.layer = OriginalLayer;

        MovementScript.enabled = true;
        AbilityScript.enabled = true;

        sr.sprite = OriginalSprite;

        FreezeFrame(0.2f);
        Door.instance.UpdateCount();


        if (IsOnPlayer)
        {
            transform.position = Vector3.zero;
        }
        MoveSpeed = StartMoveSpeed;
        MovementScript.Invoke("Healed", 0f);
        CurrentHealth = MaxHealth;

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


    private void FreezeFrame(float duration)
    {
        Time.timeScale = 0f;

        StartCoroutine(UnFreezeFrame(duration));
    }

    private IEnumerator UnFreezeFrame(float dur)
    {
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = 1f;
    }


    public void BecomeAggro()
    {
        MovementScript.Invoke("BecomeAggro", 0f);
    }
}
