using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    [SerializeField] private Wrapper twinWrapper;
    [SerializeField] private Vector2 direction;

    private void OnCollisionEnter2D(Collision2D other)
    {
        SnakePartController snakePart = other.gameObject.GetComponent<SnakePartController>();
        if (snakePart)
        {
            snakePart.transform.position = twinWrapper.transform.position + (Vector3) twinWrapper.GetDirection();
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
