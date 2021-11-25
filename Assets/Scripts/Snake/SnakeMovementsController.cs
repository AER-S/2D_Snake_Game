using System;
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
                snakePart.transform.position += new Vector3(nextStep.x, nextStep.y, 0f);
            }
            else
            {
                snakePart.transform.position = swapPosition;
                swapPosition = position;
            }
        }
    }
}
