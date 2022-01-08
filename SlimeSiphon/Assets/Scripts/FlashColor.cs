using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{

    private Material FlashRedMat, FlashWhiteMat, FlashGreenMat, OriginalMat;
    private SpriteRenderer sr;
    private Coroutine RedFlashRoutine, WhiteFlashRoutine, GreenFlashRoutine;
    [SerializeField] private float RedDuration = 0.2f, WhiteDuration = 0.2f, GreenDuration = 0.4f;

    void Start()
    {
        FlashRedMat = Resources.Load("FlashRed", typeof(Material)) as Material;
        FlashWhiteMat = Resources.Load("FlashWhite", typeof(Material)) as Material;
        FlashGreenMat = Resources.Load("FlashGreen", typeof(Material)) as Material;

        sr = GetComponent<SpriteRenderer>();
        OriginalMat = sr.material;
    }

    public void FlashRed()
    {
        if (RedFlashRoutine != null)
        {
            StopCoroutine(RedFlashRoutine);
        }

        RedFlashRoutine = StartCoroutine(FlashRedRoutine());
    }

    private IEnumerator FlashRedRoutine()
    {
        sr.material = FlashRedMat;

        yield return new WaitForSeconds(RedDuration);

        sr.material = OriginalMat;

        RedFlashRoutine = null;
    }



    public void FlashWhite()
    {
        if (WhiteFlashRoutine != null)
        {
            StopCoroutine(WhiteFlashRoutine);
        }

        WhiteFlashRoutine = StartCoroutine(FlashWhiteRoutine());
    }

    private IEnumerator FlashWhiteRoutine()
    {
        sr.material = FlashWhiteMat;

        yield return new WaitForSeconds(WhiteDuration);

        sr.material = OriginalMat;

        WhiteFlashRoutine = null;
    }





    public void FlashGreen()
    {
        if (GreenFlashRoutine != null)
        {
            StopCoroutine(GreenFlashRoutine);
        }

        GreenFlashRoutine = StartCoroutine(FlashGreenRoutine());
    }

    private IEnumerator FlashGreenRoutine()
    {
        sr.material = FlashGreenMat;

        yield return new WaitForSeconds(GreenDuration);


        sr.material = OriginalMat;

        GreenFlashRoutine = null;
    }
}
