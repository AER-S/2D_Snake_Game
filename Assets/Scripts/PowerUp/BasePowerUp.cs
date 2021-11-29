using System;
using UnityEngine;

public abstract class BasePowerUp : MonoBehaviour,PowerUp
{
    private PowerUpItem item;

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
        BoostSnake();
    }

    public abstract void BoostSnake();
}
