

using System.Collections;
using UnityEngine;

public class ScoreBooster : BasePowerUp
{
    [SerializeField] private float boostTime;
    
    
    public ScoreBooster() : base(PowerUpItem.ScoreBooster)
    {
    }
    

    public override void BoostSnake()
    {
        
        StartCoroutine(BoostScore());

    }

    IEnumerator BoostScore()
    {
        snake.PowerUpSnake(this,"ScoreBooster");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        snake.BoostScore();
        yield return new WaitForSecondsRealtime(boostTime);
        snake.StopBoostScore();
        snake.CoolDownFromPowerUP(GetCoolDownTime());
        Destroy(gameObject);
    }
}
