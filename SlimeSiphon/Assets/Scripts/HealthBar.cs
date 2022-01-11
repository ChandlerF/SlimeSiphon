using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    private Image RedFill, WhiteFill;

    private float OldHealth, MaxHealth, NewFill, NewHealth;

    [SerializeField] private float LerpTime;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Invoke("NewStart", 0.5f);
    }
    private void NewStart()
    {
        WhiteFill = transform.GetChild(0).GetComponent<Image>();
        RedFill = transform.GetChild(0).GetChild(0).GetComponent<Image>();


        Health PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        MaxHealth = PlayerHealth.MaxHealth;
        OldHealth = PlayerHealth.CurrentHealth;
        WhiteFill.fillAmount = PlayerHealth.CurrentHealth / PlayerHealth.MaxHealth;

        SetHealthBar(PlayerHealth.CurrentHealth);
    }

    private void Update()
    {
        if(OldHealth > NewHealth)
        {
            float amount = (1 * LerpTime * Time.deltaTime);
            WhiteFill.fillAmount -= amount / MaxHealth;
            OldHealth -= amount;
        }
    }

    public void SetHealthBar(float newHealth)
    {
        NewHealth = newHealth;

        RedFill.fillAmount = NewHealth / MaxHealth;

        //When the player gets healed:
        if(NewHealth > OldHealth + .1)
        {
            OldHealth = NewHealth;
            WhiteFill.fillAmount = OldHealth / MaxHealth;
        }
    }
}
