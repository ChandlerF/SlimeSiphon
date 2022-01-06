using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float Time;
    void Start()
    {
        Destroy(gameObject, Time);
    }
}
