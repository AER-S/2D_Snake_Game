using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PowerUpDistribution : ICloneable
{
    [SerializeField] private BasePowerUp powerUp;
    [SerializeField] private int quantity;

    public void Copy(PowerUpDistribution _powerUpDistribution)
    {
        powerUp = _powerUpDistribution.GetPowerUp();
        quantity = _powerUpDistribution.GetQuantity();
    }

    PowerUpDistribution(PowerUpDistribution _powerUpDistribution)
    {
        powerUp = _powerUpDistribution.GetPowerUp();
        quantity = _powerUpDistribution.GetQuantity();
    }
    public PowerUpDistribution(){}

    public BasePowerUp GetPowerUp()
    {
        return powerUp;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public object Clone()
    {
        PowerUpDistribution newItem = new PowerUpDistribution(this);
        return newItem;
    }
}
