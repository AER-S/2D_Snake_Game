using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField] private int index;
    [SerializeField] private List<FoodDistribution> foodDistributions =new List<FoodDistribution>();
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
    
    private void OnValidate()
    {
        objectives.Update();
    }
    
}
