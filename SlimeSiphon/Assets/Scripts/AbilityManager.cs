using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public MonoBehaviour AbilityOne, AbilityTwo;

    public float StartTimerOne, StartTimerTwo;
    private float TimerOne, TimerTwo;

    private bool CanUseOne = false, CanUseTwo = false, TouchingBody = false, HasOnlyOneAbility = true;

    private GameObject InteractText, DeadBody, Canvas;

    [SerializeField] private bool FirstIsTop = true;

    private Transform AbilityParent;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TimerOne = StartTimerOne;

        TimerTwo = StartTimerTwo;

        InteractText = transform.GetChild(0).gameObject;

        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        AbilityParent = Canvas.transform.GetChild(0).GetChild(0);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanUseOne)
        {
            AbilityOne.Invoke("Ability", 0f);
            TimerOne = StartTimerOne;
            //Screenshake for every ability use?

            CanUseOne = false;
        }



        else if (Input.GetMouseButtonDown(1) && CanUseTwo && !HasOnlyOneAbility)
        {
            AbilityTwo.Invoke("Ability", 0f);
            TimerTwo = StartTimerTwo;

            CanUseTwo = false;
        }



        else if (Input.GetKeyDown(KeyCode.F) && TouchingBody)
        {
            Health DeadBodyHealth = DeadBody.GetComponent<Health>();

            GetComponent<Health>().Heal(DeadBodyHealth.MaxHealth / 2); //Should also say that you've healed from it already


            Component NewScript = gameObject.AddComponent(DeadBodyHealth.AbilityScript.GetType());

            if (FirstIsTop && !HasOnlyOneAbility)
            {
                Destroy(AbilityOne);
                AbilityOne = (MonoBehaviour)NewScript;
            }
            else
            {
                Destroy(AbilityTwo);
                AbilityTwo = (MonoBehaviour)NewScript;
            }

            SetAbilitySprite();

            HasOnlyOneAbility = false;


            DeadBody.tag = "Untagged";              //--------------- Need to change this, so you can swap to your old ability, but not heal again
            DisableInteractive();
        }



        else if (Input.GetKeyDown(KeyCode.Q))
        {
            FlipAbility();
        }



        if (TimerOne > 0 && CanUseOne == false)
        {
            TimerOne -= Time.deltaTime;
        }
        else
        {
            CanUseOne = true;
        }



        if(TimerTwo > 0 && CanUseTwo == false)
        {
            TimerTwo -= Time.deltaTime;
        }
        else
        {
            CanUseTwo = true;
        }
    }






    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Dead"))
        {
            InteractText.SetActive(true);
            DeadBody = col.gameObject;
            TouchingBody = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Dead"))
        {
            DisableInteractive();
        }
    }


    private void DisableInteractive()
    {

        InteractText.SetActive(false);
        DeadBody = null;
        TouchingBody = false;
    }


    private void FlipAbility()
    {
        GameObject TopAbility = AbilityParent.GetChild(0).gameObject;
        Image TopImg = TopAbility.GetComponent<Image>();

        GameObject LeftClick = AbilityParent.GetChild(1).gameObject;
        Image LClickImg = LeftClick.GetComponent<Image>();

        GameObject BottomAbility = AbilityParent.GetChild(2).gameObject;
        Image BottomImg = BottomAbility.GetComponent<Image>();

        GameObject RightClick = AbilityParent.GetChild(3).gameObject;
        Image RClickImg = RightClick.GetComponent<Image>();


        Sprite TempAbility = TopImg.sprite;

        TopImg.sprite = BottomImg.sprite;
        BottomImg.sprite = TempAbility;


        Sprite TempClick = LClickImg.sprite;

        LClickImg.sprite = RClickImg.sprite;
        RClickImg.sprite = TempClick;

        if (FirstIsTop)
        {
            FirstIsTop = false;
        }
        else
        {
            FirstIsTop = true;
        }
    }


    private void SetAbilitySprite()       //Whenever I change the right click, the UI gets swapped
    {
        Sprite img = Resources.Load("Ability" + DeadBody.GetComponent<Health>().AbilityScript.GetType().Name, typeof(Sprite)) as Sprite;
        GameObject TopAbility = AbilityParent.GetChild(0).gameObject;
        Image TopImg = TopAbility.GetComponent<Image>();

        GameObject BottomAbility = AbilityParent.GetChild(2).gameObject;
        Image BottomImg = BottomAbility.GetComponent<Image>();
        
        if (!HasOnlyOneAbility)
        {
            TopImg.sprite = img;
        }
        else
        {
            BottomImg.sprite = img;
        }
    }
}
