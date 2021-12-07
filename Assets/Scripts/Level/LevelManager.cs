using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private float foodSpawningTime;
    [SerializeField] private float foodSpawingTimeRange;
    [SerializeField] private float powerUpSpawingTime;
    [SerializeField] private float powerUpSpawingTimeRange;
    private List<FoodDistribution> levelFoodDistributions = new List<FoodDistribution>();
    private List<PowerUpDistribution> levelPowerUpDistributions = new List<PowerUpDistribution>();
    private List<ObstaclesDistribution> levelObstaclesDistributions = new List<ObstaclesDistribution>();
    private Vector2 grid = new Vector2(30, 14);
    private float foodTimeCounter;
    private float powerUpTimeCounter;
    private List<Vector3> availablePositions = new List<Vector3>();
    
    private LevelManager instance;
    public LevelManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        CopyList(levelFoodDistributions,level.GetFoodDistributions());
        CopyList(levelPowerUpDistributions,level.GetPowerUpDistributions());
        CopyList(levelObstaclesDistributions, level.GetObstaclesDistributions());
        ResetTimer(ref foodTimeCounter,foodSpawningTime,foodSpawingTimeRange);
        ResetTimer(ref powerUpTimeCounter, powerUpSpawingTime,foodSpawningTime);
        ResetAvailablePositions();
    }

    private void Update()
    {
        if (foodTimeCounter>0)
        {
            RunTimer(ref foodTimeCounter);
        }
        else
        {
            HandleFoodDistribution();
        }

        if (powerUpTimeCounter>0)
        {
            RunTimer(ref powerUpTimeCounter);
        }
        else
        {
            HandlePowerUpsDistribution();
        }
    }

    void ResetAvailablePositions()
    {
        int withHalf = (int)grid.x / 2;
        int heightHalf = (int) grid.y / 2;
        availablePositions.Clear();
        for (int i = -withHalf; i <= withHalf; i++)
        {
            for (int j =-heightHalf ; j <= heightHalf; j++)
            {
                availablePositions.Add(new Vector3(i,j,0));
            }
            
        }
    }
    

    void HandleFoodDistribution()
    {
        if (levelFoodDistributions.Count!=0)
        {
            int foodDistributionIndex = Random.Range(0, levelFoodDistributions.Count);
            FoodDistribution foodDistribution = levelFoodDistributions[foodDistributionIndex];
            SpawnItem(foodDistribution.GetFood());
            levelFoodDistributions[foodDistributionIndex].DecreaseQuantity();
            if (foodDistribution.GetQuantity()<=0)
            {
                levelFoodDistributions.Remove(foodDistribution);
            }
        }
        ResetTimer(ref foodTimeCounter,foodSpawningTime, foodSpawingTimeRange);
    }

    void HandlePowerUpsDistribution()
    {
        if (levelPowerUpDistributions.Count!=0)
        {
            int powerUpsDistributionIndex = Random.Range(0, levelPowerUpDistributions.Count);
            PowerUpDistribution powerUpDistribution = levelPowerUpDistributions[powerUpsDistributionIndex];
            SpawnItem(powerUpDistribution.GetPowerUp());
            levelPowerUpDistributions[powerUpsDistributionIndex].DecreaseQuantity();
            if (powerUpDistribution.GetQuantity()<= 0)
            {
                levelPowerUpDistributions.Remove(powerUpDistribution);
            }
        }
        ResetTimer(ref powerUpTimeCounter, powerUpSpawingTime, powerUpSpawingTimeRange);
    }
    void SpawnItem<T>(T _food) where T : MonoBehaviour
    {
        ResetAvailablePositions();
        RemoveSnakePositions();
        Vector3 spawnPosition = ChooseARandomAvailablePosition();
        Instantiate(_food, spawnPosition, Quaternion.identity);
    }
    
    

    private Vector3 ChooseARandomAvailablePosition()
    {
        int range = availablePositions.Count;
        int index = Random.Range(0, range);
        return availablePositions[index];
    }

    private void RemoveSnakePositions()
    {
        List<SnakePartController> snakeParts = SnakeController.Instance.GetSnakeParts();
        foreach (SnakePartController snakePart in snakeParts)
        {
            availablePositions.Remove(snakePart.transform.position);
        }
    }

    void ResetTimer(ref float _timer, float _resetValue, float _range)
    {
        _timer = _resetValue + Random.Range(-1f, 1f) * _range * 0.5f;
    }

    void RunTimer(ref float _timer)
    {
        _timer -= Time.deltaTime;
        
    }

    void CopyList<T>(List<T> _targetList, List<T> _originalList) where T: ICloneable,new()
    {
        foreach (T item in _originalList)
        {
            object newItem = item.Clone();
            _targetList.Add((T)newItem);
        }
    }
}


