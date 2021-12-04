using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : BaseObstacle
{
    public Fence() : base(ObstacleItem.fence) {}

    public override void HinderSnake()
    {
        Debug.Log("Hit Fence");
        SnakeController.Instance.KillSnake();
    }
}
