using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseFood : MonoBehaviour,Food
{
    private FoodItem foodName;
    private FoodType foodType;
    [SerializeField] private int foodValue;
    [SerializeField] private float lifeTime;
    [SerializeField] private int growthPoints;

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

    private void SetType(FoodType _type)
    {
        foodType = _type;
    }
    public FoodItem GetName()
    {
        return foodName;
    }

    public int GetFoodValue()
    {
        return foodValue;
    }

    public FoodType GetFoodType()
    {
        return foodType;
        
    }

    public int GetGrowthPoints()
    {
        return growthPoints;
        
    }

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    public BaseFood(FoodItem _name, FoodType _type)
    {
        SetName(_name);
        SetType(_type);
    }
    public abstract void FeedSnake();
}