using System;
using System.Collections.Generic;
using UnityEngine;


public class SnakeController : MonoBehaviour
{
    [SerializeField] private SnakePartController snakePartPrefab;
    [SerializeField] private int snakePartsStartingNumber = 2;
    private List<SnakePartController> snakeParts=new List<SnakePartController>();
    private float partSize;
    private bool isAlive;
    

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
        CheckSnakePartsSatringNumber();
        StartSnake();
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
    }
    void AddNewSnakePart(int _index, Vector3 _position)
    {
        SnakePartController newSnakePart = Instantiate(snakePartPrefab, this.transform, false);
        newSnakePart.transform.position = _position;
        snakeParts.Insert(_index,newSnakePart);
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

    #endregion

    public void KillSnake()
    {
        isAlive = false;
        Die.Invoke();
    }

    #endregion
}
