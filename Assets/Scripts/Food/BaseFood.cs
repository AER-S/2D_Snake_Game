using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseFood : MonoBehaviour,Food
{
    private FoodItem name;
    private int foodValue;

    private void SetName(FoodItem _name)
    {
        name = _name;
    }

    private void SetValue(int _amount)
    {
        foodValue = _amount;
    }

    public FoodItem GetName()
    {
        return name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SnakePartController snakePart = other.gameObject.GetComponent<SnakePartController>();
        if (snakePart)
        {
            FeedSnake();
            Destroy(gameObject);
        }
    }

    public int GetFoodValue()
    {
        return foodValue;
    }


    public BaseFood(FoodItem _name, int _foodvalue)
    {
        SetName(_name);
        SetValue(_foodvalue);
    }
    public abstract void FeedSnake();
}
