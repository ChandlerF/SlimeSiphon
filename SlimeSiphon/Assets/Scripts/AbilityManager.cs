using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public MonoBehaviour AbilityOne, AbilityTwo;

    public float StartTimerOne, StartTimerTwo;
    private float TimerOne, TimerTwo;

    private bool CanUseOne = false, CanUseTwo = false;

    private bool TouchingBody = false;

    private GameObject InteractText, DeadBody, Canvas;

    void Start()
    {
        TimerOne = StartTimerOne;

        TimerTwo = StartTimerTwo;

        InteractText = transform.GetChild(0).gameObject;

        Canvas = GameObject.FindGameObjectWithTag("Canvas");
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
        else if (Input.GetMouseButtonDown(1) && CanUseTwo)
        {
            AbilityTwo.Invoke("Ability", 0f);
            TimerTwo = StartTimerTwo;

            CanUseTwo = false;
        }
        else if (Input.GetKeyDown(KeyCode.F) && TouchingBody)
        {
            //Debug.Log("My Ability");
            GetComponent<Health>().Heal(DeadBody.GetComponent<Health>().MaxHealth / 2);


            DeadBody.tag = "Untagged";
            DisableInteractive();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Transform AbilityParent = Canvas.transform.GetChild(0).GetChild(0);

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
}
