using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : BaseFood
{
    private Apple() : base(FoodItem.apple,FoodType.MassGainer) {}
    

    public override void FeedSnake()
    {
        SnakeController.Instance.EatFood("Apple",GetFoodValue(),GetFoodType());
    }
}