using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool wrap;
    private Vector2 wrapDirection = Vector2.zero;
    
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
        wrap = false;
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

    bool CheckForObjectsAhead()
    {
        Vector3 headPos3D = snakeParts[0].transform.position;
        Vector2 headPos2D = new Vector2(headPos3D.x, headPos3D.y);
        RaycastHit2D objectAhead = Physics2D.BoxCast(headPos2D, new Vector2(headColliderBounds.x, headColliderBounds.y),
            0f, direction, 1f,
            obstacleLayerMask);
        if (objectAhead)
        {
            if (CheckForObstacles(objectAhead))
            {
                return true;
            }

            if (CheckForWrappers(objectAhead))
            {
                return true;
            }
        }

        return false;
    }

    bool CheckForObstacles(RaycastHit2D _objectAhead)
    {
        Obstacle obstacle = _objectAhead.collider.GetComponent<Obstacle>();
        if (obstacle!=null)
        {
            obstacle.HinderSnake();
            return true;
        }

        return false;
    }

    bool CheckForWrappers(RaycastHit2D _objectAhead)
    {
        Wrapper wrapper = _objectAhead.collider.GetComponent<Wrapper>();
        if (wrapper != null)
        {
            Wrap(wrapper);
            return true;

        } 
        return false;
    }

    void Wrap(Wrapper _wrapper)
    {
        Debug.Log("Hit Wrapper");
        Wrapper twin = _wrapper.GetTwin();
        Vector3 direction3D = twin.transform.position+(Vector3)twin.GetDirection() - _wrapper.transform.position-(Vector3)_wrapper.GetDirection();
        wrapDirection = new Vector2(direction3D.x, direction3D.y);
        wrap = true;
    }
    void Move()
    {
        CheckForObjectsAhead();
        if (!(snake.GetIsAlive()))
        {
            return;
        }

        

        if (snake.GetIsAlive())
        {
            Vector3 swapPosition = snakeParts[0].transform.position;
            foreach (SnakePartController snakePart in snakeParts)
            {
                int index = snakeParts.IndexOf(snakePart);
                Vector3 position = snakePart.transform.position;
                if (index ==0)
                {
                    Vector2 nextStep = ((wrap)?wrapDirection:direction) * snake.GetStepSize();
                    snakePart.transform.position += (Vector3) nextStep;
                    if (wrap) wrap = false;
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
