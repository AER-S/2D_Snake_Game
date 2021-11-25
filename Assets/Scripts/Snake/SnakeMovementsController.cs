using System;
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
        InvokeRepeating("Move", stepTime, stepTime);
    }

    void Move()
    {
        
    }
}
