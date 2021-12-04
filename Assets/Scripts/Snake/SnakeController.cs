using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private SnakePartController snakePartPrefab;
    [SerializeField] private int snakePartsStartingNumber = 2;
    [SerializeField] private int growthStep = 2;
    [SerializeField] private float speed = 0.33f;
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
    
    public event Action<float> SpeedChanged = delegate(float _newspeed) {  };

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

    void UpdateAttribute(ref int _attribute,FoodType _foodType, int _amount)
    {
        switch (_foodType)
        {
            case FoodType.MassGainer:
                _attribute += _amount;
                break;
            case FoodType.MassBurner:
                _attribute -= _amount;
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

    #region Setters

    public void SetSpeed(float _amount)
    {
        speed = _amount;
        SpeedChanged.Invoke(_amount);
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

    public void KillSnake()
    {
        isAlive = false;
        Die.Invoke();
    }

    public void EatFood(BaseFood _food, string _name)
    {
        int foodValue = _food.GetFoodValue();
        FoodType foodType = _food.GetFoodType();
        UpdateAttribute(ref score, foodType, foodValue);
        UpdateAttribute(ref growthPoints, foodType, _food.GetGrowthPoints());
        HandleLength();
        Eat.Invoke(_name, foodValue);
    }

    #endregion

    public void PowerUpSnake(string _powerUpName)
    {
        PowerUp.Invoke(_powerUpName);
    }
}
