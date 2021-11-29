using UnityEngine;

public class SnakePartController : BaseObstacle
{
    public SnakePartController() : base(ObstacleItem.snakePart)
    {
    }

    public override void HinderSnake()
    {
        Debug.Log("hitting itself");
        //SnakeController.Instance.KillSnake();
        //Decrezsehealt();
    }
}
