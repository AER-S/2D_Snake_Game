using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(SnakeController))]
public class SnakeMovementsController : MonoBehaviour
{
    private float stepTime;
    private bool move;
    [SerializeField] private LayerMask obstacleLayerMask;
    private InputMaster controls;
    private Vector2 direction;
    private Vector2 lockedDirection;
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
        snake.Move += MakeItMove;
        snake.Stop += MakeItStop;
    }

    private void PauseGame()
    {
        LevelManager.Instance.PauseGame();
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Snake.Movments.performed -= context => GetNewDirection(context.ReadValue<Vector2>());
            controls.Disable();
            
        }
        snake.Move -= MakeItMove;
        snake.Stop -= MakeItStop;
        snake.SpeedChanged -= CheckSpeed;
    }
    private void Start()
    {
        direction = Vector2.right;
        lockedDirection = direction;
        snake = SnakeController.Instance;
        snakeParts = snake.GetSnakeParts();
        headColliderBounds = snake.GetBounds();
        stepTime = snake.GetSpeed();
        wrap = false;
        move = true;
        ResetTimeCounter();
    }

    private void Update()
    {
        if (move)
        {
            if (timeCounter>0f)
            {
                timeCounter -= Time.deltaTime;
                return;
            }
            Move();
            ResetTimeCounter();
        }
    }

    #endregion

    #region Private Functions

    private void GetNewDirection(Vector2 _newDirection)
    {
        if (Math.Abs(_newDirection.x +lockedDirection.x) < 0.001f || Math.Abs(_newDirection.y +lockedDirection.y) < 0.001f) return;
        direction = _newDirection;
    }

    bool CheckForObjectsAhead()
    {
        Vector3 headPos3D = snakeParts[0].transform.position;
        Vector2 headPos2D = new Vector2(headPos3D.x, headPos3D.y);

        RaycastHit2D objectAhead = Physics2D.BoxCast(headPos2D, new Vector2(headColliderBounds.x, headColliderBounds.y)* 0.5f,

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
            lockedDirection = direction;
            Vector3 swapPosition = snakeParts[0].transform.position;
            snakeParts[0].transform.position += (Vector3) (((wrap)?wrapDirection:direction) * snake.GetStepSize());
            SnakePartController lastSnakePart = snakeParts[^1];
            snakeParts.RemoveAt(snakeParts.Count-1);
            
            lastSnakePart.transform.position = swapPosition;
            
            snakeParts.Insert(1,lastSnakePart);
            wrap = false;
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

    void MakeItMove()
    {
        move = true;
    }

    void MakeItStop()
    {
        move = false;
    }

    #endregion

    #region Public Functions

    

    #endregion
}
