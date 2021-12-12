
using UnityEngine;
[System.Serializable]
public class EatXPowerUpNTimes : BaseObjective
{
    [SerializeField] private PowerUpItem powerUpItem;
    [SerializeField] private int nTimes;

    private SnakeController snake;
    private int currentEaten;

    public EatXPowerUpNTimes()
    {
        currentEaten = 0;
    }

    public override void ResetObjective()
    {
        complete = false;
        currentEaten = 0;
    }

    public override string Describe()
    {
        return "Eat " + nTimes + " pieces of " + powerUpItem + ": " + currentEaten + "/" + nTimes;
    }

    public override void Subscribe()
    {
        snake = SnakeController.Instance;
        snake.PowerUp += CheckEatenFood;
    }

    public override void Unsubscribe()
    {
        snake.PowerUp -= CheckEatenFood;
    }

    public void CheckEatenFood(BasePowerUp _eatenPowerUp, string _foodName)
    {
        if (_eatenPowerUp.GetItem()==powerUpItem)
        {
            currentEaten++;
        }
        UpdateStatus();
    }

    public override void UpdateStatus()
    {
        if (currentEaten<nTimes)
        {
            return;
        }

        if (!GetStatus())
        {
            Complete();
        }
    }
}
