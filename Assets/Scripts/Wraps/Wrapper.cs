using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    [SerializeField] private Wrapper twinWrapper;
    [SerializeField] private Vector2 direction;

    
    public Wrapper GetTwin()
    {
        return twinWrapper;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }
}
