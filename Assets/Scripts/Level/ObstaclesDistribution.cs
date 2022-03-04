using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObstaclesDistribution : ICloneable
{
    [SerializeField] private BaseObstacle obstacle;
    [SerializeField] private Vector2[] positions;
    
    public ObstaclesDistribution(){}

    public ObstaclesDistribution(ObstaclesDistribution _obstaclesDistribution)
    {
        obstacle = _obstaclesDistribution.GetObstacle();
        Vector2[] originalPositions = _obstaclesDistribution.GetPositions();
        positions = new Vector2[originalPositions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = originalPositions[i];
        }
    }

    public BaseObstacle GetObstacle()
    {
        return obstacle;
    }

    public Vector2[] GetPositions()
    {
        return positions;
    }

    public object Clone()
    {
        ObstaclesDistribution newItem = new ObstaclesDistribution(this);
        return newItem;
    }
}
