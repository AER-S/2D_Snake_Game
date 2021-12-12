using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int index;
    [SerializeField] private float time;
    [SerializeField] private float foodSpawningTime;
    [SerializeField] private float foodSpawningTimeRange;
    [SerializeField] private List<FoodDistribution> foodDistributions =new List<FoodDistribution>();
    [SerializeField] private float powerUpSpawningTime;
    [SerializeField] private float powerUpSpawningTimeRange;
    [SerializeField] private List<PowerUpDistribution> powerUpDistributions = new List<PowerUpDistribution>();
    [SerializeField] private List<ObstaclesDistribution> obstaclesDistributions = new List<ObstaclesDistribution>();

    [SerializeField] private Objectives objectives;
    
    [Serializable]
    class Objectives
    {
        [SerializeField] private Objective objective_1;
        [SerializeField] private Objective objective_2;
        [SerializeField] private Objective objective_3;

        public void Update()
        {
            objective_1.Update();
            objective_2.Update();
            objective_3.Update();
        }

        public Objective GetObjective(int _index)
        {
            Objective chosen = null;
            switch (_index)
            {
                case 0:
                    chosen =objective_1;
                    break;
                case 1:
                    chosen = objective_2;
                    break;
                case 2:
                    chosen= objective_3;
                    break;

            }

            return chosen;
        }

    }
    
    
   

    

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

    public BaseObjective[] GetObjectives()
    {
        BaseObjective[] baseObjectives = new BaseObjective[3];
        for (int i = 0; i < baseObjectives.Length; i++)
        {
            
            baseObjectives[i] = objectives.GetObjective(i).GetObjective();
        }

        return baseObjectives;
    }
    
    private void OnValidate()
    {
        objectives.Update();
    }

    public float GetFoodSpawningTime()
    {
        return foodSpawningTime;
    }

    public float GetFoodSpawningTimeRange()
    {
        return foodSpawningTimeRange;
    }

    public float GetPowerUpSpawningTime()
    {
        return powerUpSpawningTime;
    }

    public float GetPowerUpSpawningTimeRange()
    {
        return powerUpSpawningTimeRange;
    }

    public float GetTime()
    {
        return time;
    }
    
}
