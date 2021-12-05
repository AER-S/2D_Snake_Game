using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class SnakeMovementsController : MonoBehaviour
{
    [SerializeField] private float stepTime;
    [SerializeField] private LayerMask obstacleLayerMask;
    private InputMaster controls;
    private Vector2 direction;
    private Vector2 lockedDirection;
    private SnakeController snake;
    private List<SnakePartController> snakeParts;
    private Vector3 headColliderBounds;
    private float timeCounter;
    
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
        lockedDirection = direction;
        snake = SnakeController.Instance;
        snakeParts = snake.GetSnakeParts();
        headColliderBounds = snake.GetBounds();
        ResetTimeCounter();
    }

    private void Update()
    {
        if (timeCounter>=0f)
        {
            timeCounter -= Time.deltaTime;
            return;
        }
        Move();
        ResetTimeCounter();
    }

    #endregion

    #region Private Functions

    private void GetNewDirection(Vector2 _newDirection)
    {
        if (Math.Abs(_newDirection.x +lockedDirection.x) < 0.001f || Math.Abs(_newDirection.y +lockedDirection.y) < 0.001f) return;
        direction = _newDirection;
    }

    bool CheckForObstacles()
    {
        Vector3 headPos3D = snakeParts[0].transform.position;
        Vector2 headPos2D = new Vector2(headPos3D.x, headPos3D.y);
        RaycastHit2D objectAhead = Physics2D.BoxCast(headPos2D, new Vector2(headColliderBounds.x, headColliderBounds.y)* 0.5f,
            0f, direction, 1f,
            obstacleLayerMask);
        if (objectAhead)
        {
            Obstacle obstacle = objectAhead.collider.GetComponent<Obstacle>();
            if (obstacle!=null)
            {
                obstacle.HinderSnake();
                return true;
            }
        }

        return false;
    }
    void Move()
    {
        if (!(snake.GetIsAlive()))
        {
            return;
        }

        CheckForObstacles();

        if (snake.GetIsAlive())
        {
            lockedDirection = direction;
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
    }

    void ResetTimeCounter()
    {
        timeCounter = stepTime;
    }

    #endregion

    #region Public Functions

    

    #endregion
}
