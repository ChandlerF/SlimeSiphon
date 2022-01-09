using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int Minimum, Maximum, Current;
    [SerializeField] private Image Mask, Fill;
    [SerializeField] private Color Color;



    void Update()
    {
        GetCurrentFill();
    }


    private void GetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;

        float fillAmount = currentOffset / maximumOffset;
        Mask.fillAmount = fillAmount;

        Fill.color = Color;
    }
}
