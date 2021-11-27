using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : BaseFood
{
    private Apple() : base(FoodItem.apple, 10) {}
    

    public override void FeedSnake()
    {
        SnakeController.Instance.EatFood("Apple",GetFoodValue());
    }
}
