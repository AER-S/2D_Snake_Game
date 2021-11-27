using UnityEngine;

public abstract class BaseFood : MonoBehaviour, Food
{
    private string name;
    private int foodvalue;

    protected void SetName(string _name)
    {
        name = _name;
    }

    protected void SetValue(int _amount)
    {
        foodvalue = _amount;
    }

    public string GetName()
    {
        return name;
    }

    public int GetFoodValue()
    {
        return foodvalue;
    }

    public abstract void FeedSnake();
}
