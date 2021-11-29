using System;
using System.Collections;
using UnityEngine;

public class SpeedLimiter : BasePowerUp
{
    [SerializeField] private float powerUpTime;
    [SerializeField] private float speedFactor;
    private SnakeController snake;

    private void Start()
    {
        snake = SnakeController.Instance;
    }

    public SpeedLimiter() : base(PowerUpItem.speedLimiter) {}

    public override void BoostSnake()
    {
        snake.PowerUpSnake("SpeedLimiter");
        StartCoroutine(SlowDownSnake(powerUpTime, speedFactor));
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator SlowDownSnake(float _time, float _factor)
    {
        float currentSpeed = snake.GetSpeed();
        float newSpeed = currentSpeed * _factor;
        snake.SetSpeed(newSpeed);
        yield return new WaitForSecondsRealtime(_time);
        snake.SetSpeed(currentSpeed);
        Destroy(gameObject);
    }
}
