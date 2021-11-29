using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int index;
    [SerializeField] private List<FoodDistribution> foodDistributions =new List<FoodDistribution>();
    [SerializeField] private List<PowerUpDistribution> powerUpDistributions = new List<PowerUpDistribution>();
    [SerializeField] private List<ObstaclesDistribution> obstaclesDistributions = new List<ObstaclesDistribution>();

    public List<FoodDistribution> GetFoodDistributions()
    {
        return foodDistributions;
    }

    public List<PowerUpDistribution> GetPowerUpDistributions()
    {
        return powerUpDistributions;
    }

    public List<ObstaclesDistribution> GetObstaclesDistributions()
    {
        return obstaclesDistributions;
    }
}
