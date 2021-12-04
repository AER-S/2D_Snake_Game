using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private SnakePartController snakePartPrefab;
    [SerializeField] private int snakePartsStartingNumber = 2;
    [SerializeField] private int growthStep = 2;
    private List<SnakePartController> snakeParts=new List<SnakePartController>();
    private float partSize;
    private bool isAlive;
    private int score;
    private int lostScore;
    private int length;
    private int growthPoints;
    

    private static SnakeController instance;
    public static SnakeController Instance
    {
        get { return instance; }
    }


    #region Events

    #region Public Events

    public event Action Die = delegate {  };
    public event Action<string,int> Eat = delegate(string _foodName, int _foodValue) {  };
    public event Action<int> Grow = delegate {  };
    public event Action<int> Shrink = delegate {  };
    public event Action<string,int> Hit = delegate(string _obstacleName, int _damage) {  };
    public event Action<string> PowerUp = delegate(string _powerUpName) {  }; 

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
        length = snakePartsStartingNumber;
        growthPoints = 0;
        CheckSnakePartsStartingNumber();
        StartSnake();
    }

    private void OnEnable()
    {
        Grow += GrowSnake;
        Shrink += ShrinkSnake;
    }

    private void OnDisable()
    {
        Grow -= GrowSnake;
        Shrink -= ShrinkSnake;
    }

    #endregion

    #region Private Functions

    void CheckSnakePartsStartingNumber()
    {
        snakePartsStartingNumber = (snakePartsStartingNumber < 1) ? 1 : snakePartsStartingNumber;
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
    }
    void AddNewSnakePart(int _index, Vector3 _position)
    {
        SnakePartController newSnakePart = Instantiate(snakePartPrefab, this.transform, false);
        newSnakePart.transform.position = _position;
        snakeParts.Insert(_index,newSnakePart);
    }

    void DeleteSnakePart(int _index)
    {
        snakeParts.Remove(snakeParts[_index]);
    }

    void UpdateScore(int _amount)
    {
        score += _amount;
    }

    private void UpdateGrowthPoints(FoodType _type)
    {
        switch (_type)
        {
            case FoodType.MassGainer:
                ++growthPoints;
                break;
            case FoodType.MassBurner:
                --growthPoints;
                break;
        }
    }

    

    void HandleLength()
    {
        int neededNewParts = growthPoints / growthStep;
        if (neededNewParts>0) Grow.Invoke(neededNewParts);
        if (neededNewParts<0) Shrink.Invoke(neededNewParts);
        length += neededNewParts;
        growthPoints -= neededNewParts;
        
    }

    void GrowSnake(int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            AddNewSnakePart(snakeParts.Count,2 * snakeParts[snakeParts.Count-1].transform.position-snakeParts[snakeParts.Count-2].transform.position);
        }
    }

    void ShrinkSnake(int _amount)
    {
        for (int i = 0; i < Mathf.Abs(_amount); i++)
        {
            Destroy(snakeParts[^1].gameObject);
            DeleteSnakePart(snakeParts.Count-1);
        }
    }

    #endregion

    #region Public Functions

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

    #endregion

    public void KillSnake()
    {
        isAlive = false;
        Die.Invoke();
    }

    public void EatFood(string _name, int _foodValue, FoodType _type)
    {
        UpdateScore(_foodValue);
        UpdateGrowthPoints(_type);
        HandleLength();
        Eat.Invoke(_name, _foodValue);
    }

    #endregion
}
