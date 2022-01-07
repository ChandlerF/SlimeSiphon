using DG.Tweening;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public TextMeshPro Text;

    void Start()
    {
        transform.GetChild(0).DOScale(new Vector3(1, 1, 1), 0.45f).SetEase(Ease.InOutBack).OnComplete(() => 
        {
            Invoke("FinishTween", 0.5f);
        });
    }



    private void FinishTween()
    {
        transform.GetChild(0).DOScale(new Vector3(.01f, .01f, .01f), 0.2f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
