using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform Player;
    private Vector3 Offset = new Vector3(0, 0, -20);
    [SerializeField] private float SmoothSpeed = 0.1f;

    private Vector3 Velocity = Vector3.zero;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Player.position;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = Player.position + Offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref Velocity, SmoothSpeed);

        transform.position = smoothedPosition;
    }
}
