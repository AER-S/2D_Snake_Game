using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : BaseObstacle
{
    [SerializeField] private string boulderName;
    public Boulder() : base(ObstacleItem.boulder)
    {
    }

    public override void HinderSnake()
    {
        throw new System.NotImplementedException();
    }
    
    
}
