using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private float foodSpawningTime;
    private List<FoodDistribution> levelFoodDistributions = new List<FoodDistribution>();
    private List<PowerUpDistribution> levelPowerUpDistributions = new List<PowerUpDistribution>();
    private List<ObstaclesDistribution> levelObstaclesDistributions = new List<ObstaclesDistribution>();
    private Vector2 grid = new Vector2(30, 14);
    private float foodTimeCounter;
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
        //levelFoodDistributions = level.GetFoodDistributions();
        CopyList<FoodDistribution>(levelFoodDistributions,level.GetFoodDistributions());
        //levelPowerUpDistributions = level.GetPowerUpDistributions();
        CopyList<PowerUpDistribution>(levelPowerUpDistributions,level.GetPowerUpDistributions());
        //levelObstaclesDistributions = level.GetObstaclesDistributions();
        CopyList<ObstaclesDistribution>(levelObstaclesDistributions, level.GetObstaclesDistributions());
        ResetFoodTimer();
        ResetAvailablePositions();
    }

    private void Update()
    {
        if (foodTimeCounter>0)
        {
            foodTimeCounter -= Time.deltaTime;
        }
        else
        {
            HandleFoodDistribution();
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
            SpawnFood(foodDistribution.GetFood());
            levelFoodDistributions[foodDistributionIndex].DecreaseQuantity();
            if (foodDistribution.GetQuantity()<=0)
            {
                levelFoodDistributions.Remove(foodDistribution);
            }
        }
        ResetFoodTimer();
    }
    void SpawnFood(BaseFood _food)
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

    void ResetFoodTimer()
    {
        foodTimeCounter = foodSpawningTime;
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


