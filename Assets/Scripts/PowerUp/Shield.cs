using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BasePowerUp
{
    [SerializeField] private string shieldName;
    [SerializeField] private int value;
    public Shield() : base(PowerUpItem.Shield)
    {
    }

    public override void BoostSnake()
    {
        SnakeController.Instance.PowerUpSnake(shieldName);
        SnakeController.Instance.AddShields(value);
        Destroy(gameObject);
    }
}
