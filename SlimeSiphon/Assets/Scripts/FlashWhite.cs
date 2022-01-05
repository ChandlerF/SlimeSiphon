using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{
    private Material FlashMat, OriginalMat;
    private SpriteRenderer sr;
    private Coroutine flashRoutine;
    [SerializeField] private float Duration = 0.45f;

    void Start()
    {
        FlashMat = Resources.Load("FlashWhite", typeof(Material)) as Material;

        sr = GetComponent<SpriteRenderer>();
        OriginalMat = sr.material;
    }
    
    public void Flash()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        sr.material = FlashMat;

        yield return new WaitForSeconds(Duration);

        sr.material = OriginalMat;

        flashRoutine = null;
    }
}
