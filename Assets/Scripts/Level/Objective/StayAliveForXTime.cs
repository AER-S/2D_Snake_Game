
using UnityEngine;
[System.Serializable]
public class StayAliveForXTime : BaseObjective
{
    [SerializeField] private float objectiveTime;
    private float startTime;
    private SnakeController snake;

    public override void ResetObjective()
    {
        complete = false;
    }

    public override string Describe()
    {
        return "Stay alive for " + objectiveTime + ": " + (Time.time-startTime) + "/" + objectiveTime;
    }

    public override void Subscribe()
    {
        snake = SnakeController.Instance;
        snake.Die += UpdateStatus;
        LevelManager.Pause += UpdateStatus;
        startTime = Time.time;
    }

    public override void Unsubscribe()
    {
        LevelManager.Pause -= UpdateStatus;
        snake.Die -= UpdateStatus;
    }

    public override void UpdateStatus()
    {
        float deathTime = Time.time;
        if (!GetStatus()&&deathTime-startTime>=objectiveTime)
        {
            Complete();
        }
    }
}
