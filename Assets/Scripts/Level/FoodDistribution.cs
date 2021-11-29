
using UnityEngine;

[System.Serializable]
public class FoodDistribution
{
   [SerializeField] private BaseFood food;
   [SerializeField] private int quantity;

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
}
