using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleItem
{
    fence,
    boulder,
    snakePart
}
public interface Obstacle
{
    void HinderSnake();
}
