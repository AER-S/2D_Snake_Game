using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class SnakeMovementsController : MonoBehaviour
{
    [SerializeField] private float stepTime;
    private InputMaster controls;
    private SnakeController snakeController;
    private Vector2 direction;
    private Action<Vector2> DirectionHandle;
    private void Awake()
    {
        snakeController = GetComponent<SnakeController>();
        controls = new InputMaster();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Snake.Movments.performed += context => GetNewDirection(context.ReadValue<Vector2>());
    }

    private void GetNewDirection(Vector2 _newDirection)
    {
        if (Math.Abs(_newDirection.x - (-direction.x)) < 0.001f || Math.Abs(_newDirection.y - (-direction.y)) < 0.001f) return;
        direction = _newDirection;
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Snake.Movments.performed -= context => GetNewDirection(context.ReadValue<Vector2>());
            controls.Disable();
        }
    }

    private void Start()
    {
        direction = Vector2.right;
        InvokeRepeating("Move", stepTime, stepTime);
    }

    void Move()
    {
        if (!snakeController.GetIsAlive())
        {
            return;
        }

        List<SnakePartController> snakeParts = snakeController.GetSnakeParts();
        Vector3 swapPosition = snakeParts[0].transform.position;
        foreach (SnakePartController snakePart in snakeParts)
        {
            int index = snakeParts.IndexOf(snakePart);
            Vector3 position = snakePart.transform.position;
            if (index ==0)
            {
                Vector2 nextStep = direction * snakeController.GetStepSize();
                StartCoroutine(LerpToPosition(snakePart,position+(Vector3)nextStep,stepTime));
            }
            else
            {
                StartCoroutine(LerpToPosition(snakePart, swapPosition, stepTime));
                swapPosition = position;
            }
        }
    }

    IEnumerator LerpToPosition(SnakePartController _snakePart, Vector3 _destination, float _time)
    {
        float timeCounter = 0;
        Vector3 startPosition = _snakePart.transform.position;
        while (timeCounter<_time)
        {
            _snakePart.transform.position = Vector3.Lerp(startPosition, _destination, timeCounter / _time);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        _snakePart.transform.position = _destination;
    }
}
