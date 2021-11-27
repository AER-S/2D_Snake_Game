using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class SnakeMovementsController : MonoBehaviour
{
    [SerializeField] private float stepTime;
    private InputMaster controls;
    private Vector2 direction;
    
    #region Unity Functions

    private void Awake()
    {
        controls = new InputMaster();
        
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Snake.Movments.performed += context => GetNewDirection(context.ReadValue<Vector2>());
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

    #endregion

    #region Private Functions

    private void GetNewDirection(Vector2 _newDirection)
    {
        if (Math.Abs(_newDirection.x - -direction.x) < 0.001f || Math.Abs(_newDirection.y - -direction.y) < 0.001f) return;
        direction = _newDirection;
    }

    bool CheckForKillingObstacles()
    {
        return false;
    }
    void Move()
    {
        SnakeController snake = SnakeController.Instance;
        if (!(snake.GetIsAlive()))
        {
            return;
        }

        if (CheckForKillingObstacles())
        {
            snake.KillSnake();
            return;
        }
        
        
        List<SnakePartController> snakeParts = snake.GetSnakeParts();
        Vector3 swapPosition = snakeParts[0].transform.position;
        foreach (SnakePartController snakePart in snakeParts)
        {
            int index = snakeParts.IndexOf(snakePart);
            Vector3 position = snakePart.transform.position;
            if (index ==0)
            {
                Vector2 nextStep = direction * snake.GetStepSize();
                snakePart.transform.position += (Vector3) nextStep;
            }
            else
            {
                snakePart.transform.position = swapPosition;
                swapPosition = position;
            }
        }
    }

    #endregion

    #region Public Functions

    

    #endregion
}
