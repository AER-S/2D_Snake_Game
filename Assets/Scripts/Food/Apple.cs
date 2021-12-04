using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : BaseFood
{
    private Apple() : base(FoodItem.apple, 10,10f) {}
    

    public override void FeedSnake()
    {
        SnakeController.Instance.EatFood("Apple",GetFoodValue());
    }
}
