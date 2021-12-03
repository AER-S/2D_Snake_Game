using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private SnakePartController snakePartPrefab;
    [SerializeField] private int snakePartsStartingNumber = 2;
    [SerializeField] private int growthStep = 20;
    private List<SnakePartController> snakeParts=new List<SnakePartController>();
    private float partSize;
    private bool isAlive;
    private int score;
    private int lostScore;
    [SerializeField]private float speed;
    private int shields;
    

    private static SnakeController instance;
    public static SnakeController Instance
    {
        get { return instance; }
    }


    #region Events

    #region Public Events

    public event Action Die = delegate {  };
    public event Action<string,int> Eat = delegate(string _foodName, int _foodValue) {  };
    public event Action Grow = delegate {  };
    public event Action<string,int> Hit = delegate(string _obstacleName, int _damage) {  };
    public event Action<string> PowerUp = delegate(string _powerUpName) {  };
    
    public event Action<float> SpeedChanged = delegate(float _newSpeed) {  }; 

    #endregion

    #endregion
    
    
    

    #region Unity Functions

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        partSize = snakePartPrefab.GetComponent<SpriteRenderer>().bounds.size.x+0.1f;
    }
    private void Start()
    {
        isAlive = true;
        score = 0;
        CheckSnakePartsSatringNumber();
        StartSnake();
    }

    private void OnEnable()
    {
        Grow += GrowSnake;
    }

    private void OnDisable()
    {
        Grow -= GrowSnake;
    }

    #endregion

    #region Private Functions

    void CheckSnakePartsSatringNumber()
    {
        snakePartsStartingNumber = (snakePartsStartingNumber < 2) ? 2 : snakePartsStartingNumber;
    }
    void StartSnake()
    {
        for (int i = 0; i < snakePartsStartingNumber; i++)
        {
            Vector3 newPosition = new Vector3(-i * partSize, 0f, 0f);
            AddNewSnakePart(i, newPosition);
            
        }
        
        Rigidbody2D snakeHead = snakeParts[0].AddComponent<Rigidbody2D>();
        snakeHead.gravityScale = 0;
        snakeParts[0].gameObject.layer = 7;
    }
    void AddNewSnakePart(int _index, Vector3 _position)
    {
        SnakePartController newSnakePart = Instantiate(snakePartPrefab, this.transform, false);
        newSnakePart.transform.position = _position;
        snakeParts.Insert(_index,newSnakePart);
    }

    void UpdateScore(int _amount)
    {
        score += _amount;
    }

    void CheckLength()
    {
        int neededNewParts = (score + lostScore - (snakeParts.Count-snakePartsStartingNumber)*growthStep) / growthStep;
        for (int i = 1; i <= neededNewParts; i++)
        {
            Grow.Invoke();
        }
    }

    void GrowSnake()
    {
        AddNewSnakePart(snakeParts.Count,2 * snakeParts[snakeParts.Count-1].transform.position-snakeParts[snakeParts.Count-2].transform.position);
    }

    void ReduceShields(int _damage)
    {
        shields -= _damage;
        if (shields<0)
        {
            shields = 0;
        }
    }

    #endregion

    #region Public Functions

    #region Setters

    public void SetSpeed(float _newSpeed)
    {
        if (speed!=_newSpeed)
        {
            SpeedChanged.Invoke(_newSpeed);
        }
        speed = _newSpeed;
    }

    #endregion
    
    #region Getters

    public float GetStepSize()
    {
        return partSize;
    }
    public List<SnakePartController> GetSnakeParts()
    {
        return snakeParts;
    }
    public bool GetIsAlive()
    {
        return isAlive;
    }

    public Vector3 GetBounds()
    {
        return snakePartPrefab.GetComponent<BoxCollider2D>().size*snakePartPrefab.transform.localScale;
    }

    public float GetSpeed()
    {
        return speed;
    }

    #endregion

    public void HitSnake(string _obstacleName, int _damage)
    {
        if (shields>0)
        {
            ReduceShields(_damage);
            Hit.Invoke(_obstacleName, _damage);
            return;
        }
        KillSnake();
    }
    public void KillSnake()
    {
        isAlive = false;
        Die.Invoke();
    }

    public void EatFood(string _name, int _foodValue)
    {
        UpdateScore(_foodValue);
        CheckLength();
        Eat.Invoke(_name, _foodValue);
    }

    public void PowerUpSnake(string _powerUpName)
    {
        PowerUp.Invoke(_powerUpName);
    }

    public void AddShields(int _amount)
    {
        shields += _amount;
    }
    #endregion
}
