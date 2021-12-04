using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseFood : MonoBehaviour,Food
{
    private FoodItem foodName;
    private int foodValue;
    private float lifeTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SnakePartController snakePart = other.gameObject.GetComponent<SnakePartController>();
        if (snakePart)
        {
            FeedSnake();
            Destroy(gameObject);
        }
    }

    private void SetName(FoodItem _name)
    {
        foodName = _name;
    }

    private void SetValue(int _amount)
    {
        foodValue = _amount;
    }

    private void SetLifeTime(float _amount)
    {
        lifeTime = _amount;
    }

    public FoodItem GetName()
    {
        return foodName;
    }

    public int GetFoodValue()
    {
        return foodValue;
    }

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    public BaseFood(FoodItem _name, int _foodvalue, float _lifeTime)
    {
        SetName(_name);
        SetValue(_foodvalue);
        SetLifeTime(_lifeTime);
    }
    public abstract void FeedSnake();
}