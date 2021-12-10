using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EatXFoodNTimes : BaseObjective
{
    [SerializeField] private FoodItem foodItem;
    [SerializeField] private int nTimes;
}
