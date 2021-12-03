using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartController : BaseObstacle
{
    public SnakePartController() : base(ObstacleItem.snakePart)
    {
    }

    public override void HinderSnake()
    {
        StartCoroutine(StopSnakePartCollision());
        SnakeController.Instance.HitSnake("Snake Part",1);
        

    }

    IEnumerator StopSnakePartCollision()
    {
        
        List<SnakePartController> snakeParts = SnakeController.Instance.GetSnakeParts();
        foreach (SnakePartController snakePart in snakeParts)
        {
            snakePart.gameObject.layer = 0;
        }

        yield return new WaitForSecondsRealtime(SnakeController.Instance.GetSpeed());
        foreach (SnakePartController snakePart in snakeParts)
        {
            snakePart.gameObject.layer = 7;
        }
    }
}
