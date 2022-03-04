
using System;
using UnityEngine;

[System.Serializable]
public class FoodDistribution : ICloneable
{
   [SerializeField] private BaseFood food;
   [SerializeField] private int quantity;
   

   FoodDistribution(FoodDistribution _foodDistribution)
   {
      food = _foodDistribution.GetFood();
      quantity = _foodDistribution.GetQuantity();
   }

   public FoodDistribution() {}

   public void DecreaseQuantity()
   {
      quantity-=1;
      Debug.Log("Quantity Deacreased");
   }
   
   public BaseFood GetFood()
   {
      return food;
   }

   public int GetQuantity()
   {
      return quantity;
   }

   public object Clone()
   {
      FoodDistribution newObject = new FoodDistribution(this);
      return newObject;
   }
}
