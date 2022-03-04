using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodItem
{
    apple,
    mushroom
}

public enum FoodType
{
    MassGainer,
    MassBurner
}
public interface Food
{
    void FeedSnake();
}
