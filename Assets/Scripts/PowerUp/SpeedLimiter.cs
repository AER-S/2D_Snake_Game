using System;
using System.Collections;
using UnityEngine;

public class SpeedLimiter : BasePowerUp
{
    [SerializeField] private float powerUpTime;
    [SerializeField] private float speedFactor;



    public SpeedLimiter() : base(PowerUpItem.speedLimiter) {}

    public override void BoostSnake()
    {

        StartCoroutine(SlowDownSnake());
        
    }

    IEnumerator SlowDownSnake()
    {
        snake.PowerUpSnake(this,"SpeedLimiter");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        float currentSpeed = snake.GetSpeed();
        float newSpeed = currentSpeed * speedFactor;
        snake.SetSpeed(newSpeed);
        yield return new WaitForSecondsRealtime(powerUpTime);
        snake.SetSpeed(currentSpeed);
        snake.CoolDownFromPowerUP(GetCoolDownTime());
        Destroy(gameObject);
    }
}
