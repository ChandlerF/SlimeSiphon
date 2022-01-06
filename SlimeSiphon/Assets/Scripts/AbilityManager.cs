using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public MonoBehaviour AbilityOne;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AbilityOne.Invoke("Ability", 0f);
            //Screenshake for every ability use?
        }
    }
}
