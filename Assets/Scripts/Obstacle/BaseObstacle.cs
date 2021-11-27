using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObstacle : MonoBehaviour, Obstacle
{
    private ObstacleItem item;

    public BaseObstacle(ObstacleItem _item)
    {
        item = _item;
    }
    public abstract void HinderSnake();
}
