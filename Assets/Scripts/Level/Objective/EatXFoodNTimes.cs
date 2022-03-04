
using UnityEngine;
[System.Serializable]
public class EatXFoodNTimes : BaseObjective
{
    [SerializeField] private FoodItem foodItem;
    [SerializeField] private int nTimes;

    private SnakeController snake;
    private int currentEaten;

    public EatXFoodNTimes()
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
        return "Eat " + nTimes + " pieces of " + foodItem + ": " + currentEaten + "/" + nTimes;
    }

    public override void Subscribe()
    {
        snake = SnakeController.Instance;
        snake.Eat += CheckEatenFood;
    }

    public override void Unsubscribe()
    {
        snake.Eat -= CheckEatenFood;
    }

    public void CheckEatenFood(BaseFood _eatenFood, string _foodName)
    {
        if (_eatenFood.GetFoodName()==foodItem)
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
