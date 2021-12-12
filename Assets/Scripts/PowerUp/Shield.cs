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
        snake.PowerUpSnake(this,shieldName);
        snake.AddShields(value);
        snake.CoolDownFromPowerUP(GetCoolDownTime());
        Destroy(gameObject);
    }
}
