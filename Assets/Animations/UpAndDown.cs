using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private float startingAngle;

    float angle;
    Vector3 startingPos;
    Vector3 newPos;

    private void Start()
    {
        angle = startingAngle;
        startingPos = transform.position;
    }

    private void Update()
    {
        newPos = startingPos;
        newPos.y += distance * Mathf.Sin(angle);
        transform.position = newPos;

        angle += speed;
    }
}
