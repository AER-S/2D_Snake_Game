using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : BaseFood
{
    private Mushroom() : base(FoodItem.mushroom,FoodType.MassBurner) {}
    

    public override void FeedSnake()
    {
        SnakeController.Instance.EatFood(this,"Mushroom");
    }
}