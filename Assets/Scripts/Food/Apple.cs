using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Apple : BaseFood
{
    private Apple() : base(FoodItem.apple,FoodType.MassGainer) {}
    

    public override void FeedSnake()
    {
        SnakeController.Instance.EatFood(this,"Apple");
    }
}