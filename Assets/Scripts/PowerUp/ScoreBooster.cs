

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
        SnakeController.Instance.PowerUpSnake("ScoreBooster");
        StartCoroutine(BoostScore());

    }

    IEnumerator BoostScore()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        SnakeController.Instance.BoostScore();
        yield return new WaitForSecondsRealtime(boostTime);
        SnakeController.Instance.StopBoostScore();
        Destroy(gameObject);
    }
}
