using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private float foodSpawningTime;
    private List<FoodDistribution> levelFoodDistributions;
    private List<PowerUpDistribution> levelPowerUpDistributions;
    private List<ObstaclesDistribution> levelObstaclesDistributions;
    private Vector2 grid = new Vector2(30, 14);
    private float foodTimeCounter;
    
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
        levelFoodDistributions = level.GetFoodDistributions();
        levelPowerUpDistributions = level.GetPowerUpDistributions();
        levelObstaclesDistributions = level.GetObstaclesDistributions();
        ResetFoodTimer();
    }

    private void Update()
    {
        if (foodTimeCounter>0)
        {
            foodTimeCounter -= Time.deltaTime;
        }
        else
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
    }

    void SpawnFood(BaseFood _food)
    {
        bool occupiedPosition = true;
        Vector2 spawnPosition =Vector2.zero;
        while (occupiedPosition)
        {
            int xPos = (int)Random.Range(-grid.x / 2, grid.x / 2);
            int yPos = (int)Random.Range(-grid.y / 2, grid.y / 2);
            spawnPosition = new Vector2(xPos, yPos);
            occupiedPosition = CheckIfInSnake(spawnPosition);
        }

        Instantiate(_food, spawnPosition, Quaternion.identity);
    }

    void ResetFoodTimer()
    {
        foodTimeCounter = foodSpawningTime;
    }

    bool CheckIfInSnake(Vector2 _position)
    {
        bool isInSnake = false;
        List<SnakePartController> snakeParts = SnakeController.Instance.GetSnakeParts();
        foreach (SnakePartController snakePart in snakeParts)
        {
            Vector3 snakePartPosition = snakePart.transform.position;
            isInSnake = (snakePartPosition.x - _position.x < 0.001f && snakePartPosition.y - _position.y < 0.001f);
        }

        return isInSnake;
    }
}
