using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public MonoBehaviour AbilityOne, AbilityTwo;

    public float StartTimerOne, StartTimerTwo;
    private float TimerOne, TimerTwo;

    private bool CanUseOne = false, CanUseTwo = false;

    private bool TouchingBody = false;

    private GameObject InteractText;

    void Start()
    {
        TimerOne = StartTimerOne;

        TimerTwo = StartTimerTwo;

        InteractText = transform.GetChild(0).gameObject;
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
            Debug.Log("My Ability");
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
            TouchingBody = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Dead"))
        {
            InteractText.SetActive(false);
            TouchingBody = false;
        }
    }
}
