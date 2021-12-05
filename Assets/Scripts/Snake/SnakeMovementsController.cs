using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeController))]
public class SnakeMovementsController : MonoBehaviour
{
    private float stepTime;
    [SerializeField] private LayerMask obstacleLayerMask;
    private InputMaster controls;
    private Vector2 direction;
    private SnakeController snake;
    private List<SnakePartController> snakeParts;
    private Vector3 headColliderBounds;
    private float timeCounter;
    
    #region Unity Functions

    private void Awake()
    {
        controls = new InputMaster();
        snake = SnakeController.Instance;
    }
    private void OnEnable()
    {
        controls.Enable();
        controls.Snake.Movments.performed += context => GetNewDirection(context.ReadValue<Vector2>());
        snake.SpeedChanged += CheckSpeed;
    }
    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Snake.Movments.performed -= context => GetNewDirection(context.ReadValue<Vector2>());
            controls.Disable();
        }

        snake.SpeedChanged -= CheckSpeed;
    }
    private void Start()
    {
        direction = Vector2.right;
        snakeParts = snake.GetSnakeParts();
        headColliderBounds = snake.GetBounds();
        stepTime = snake.GetSpeed();
        ResetTimeCounter();
    }

    private void Update()
    {
        if (timeCounter>0f)
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
        if (Math.Abs(_newDirection.x - -direction.x) < 0.001f || Math.Abs(_newDirection.y - -direction.y) < 0.001f) return;
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

    void CheckSpeed(float _newSpeed)
    {
        stepTime = _newSpeed; 
    }

    #endregion

    #region Public Functions

    

    #endregion
}
