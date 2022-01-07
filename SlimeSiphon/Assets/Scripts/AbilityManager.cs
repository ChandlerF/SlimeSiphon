using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public MonoBehaviour AbilityOne, AbilityTwo;

    public float StartTimerOne, StartTimerTwo;
    private float TimerOne, TimerTwo;

    private bool CanUseOne = false, CanUseTwo = false;

    void Start()
    {
        TimerOne = StartTimerOne;

        TimerTwo = StartTimerTwo;
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



        if(TimerOne > 0 && CanUseOne == false)
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
}
