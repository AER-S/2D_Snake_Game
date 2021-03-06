using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour,PowerUp
{
    [SerializeField] private float coolDownTime = 3f;

    protected SnakeController snake;
    private PowerUpItem item;
    private bool powerUpStillRunning;
    private void OnEnable()
    {
        snake = SnakeController.Instance;
        snake.CoolDown += CoolDown;
        snake.PowerUp += PowerUpRunning;
    }

    private void OnDisable()
    {
        snake.CoolDown -= CoolDown;
        snake.PowerUp -= PowerUpRunning;
    }

    private void Start()
    {
        powerUpStillRunning = false;
    }

    public BasePowerUp(PowerUpItem _item)
    {
        item = _item;
    }

    public PowerUpItem GetItem()
    {
        return item;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!powerUpStillRunning)
        {
            BoostSnake();
        }
    }

    void CoolDown(float _time)
    {
        StartCoroutine(CoolingDown(_time));
    }

    void PowerUpRunning(BasePowerUp _runningPowerUp, string _runningPowerUpName)
    {
        powerUpStillRunning = true;
    }

    IEnumerator CoolingDown(float _time)
    {
        yield return new WaitForSecondsRealtime(_time);
        powerUpStillRunning = false;
    }

    public float GetCoolDownTime()
    {
        return coolDownTime;
        
    }

    public abstract void BoostSnake();
}
