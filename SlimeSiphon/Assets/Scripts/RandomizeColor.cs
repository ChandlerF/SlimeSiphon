using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    private float x, y, z;
    void Start()
    {   
        //Max = 255     Min 122     Other between the 2

        int i = Random.Range(0, 5);

        if(i == 0)
        {
            x = 255;
            y = Random.Range(122, 255);
            z = 122;
        } 
        else if(i == 1)
        {
            x = 255;
            y = 122;
            z = Random.Range(122, 255);
        } 
        else if(i == 2)
        {
            x = Random.Range(122, 255);
            y = 255;
            z = 122;
        }
        else if(i == 3)
        {
            x = 122;
            y = 255;
            z = Random.Range(122, 255);
        } else if (i == 4)
        {
            x = 122;
            y = Random.Range(122, 255);
            z = 255;
        }
        else
        {
            x = Random.Range(122, 255);
            y = 122;
            z = 255;
        }

        Color newColor = new Color(x/255, y/255, z/255, 255/255);

        GetComponent<SpriteRenderer>().color = newColor;
    }
}
