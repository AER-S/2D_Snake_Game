

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreBooster : BasePowerUp
{
    [SerializeField] private float boostTime;
    private SnakeController snake;
    
    public ScoreBooster() : base(PowerUpItem.ScoreBooster)
    {
    }
    

    public override void BoostSnake()
    {
        snake = SnakeController.Instance;
        StartCoroutine(BoostScore());

    }

    IEnumerator BoostScore()
    {
        snake.PowerUpSnake("ScoreBooster");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        snake.BoostScore();
        yield return new WaitForSecondsRealtime(boostTime);
        snake.StopBoostScore();
        snake.CoolDownFromPowerUP(GetCoolDownTime());
        Destroy(gameObject);
    }
}
