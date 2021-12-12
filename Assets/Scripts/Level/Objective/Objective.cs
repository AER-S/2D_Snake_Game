using System;
using UnityEngine;

public enum ObjectiveType
{
    //None,
    eatXFoodNTimes,
    reachXLength,
    eatXPowerUpNTimes,
    StayAliveForXTime
}



[Serializable]
public class Objective 
{
    
    [SerializeField] private ObjectiveType type;
    [SerializeReference] private BaseObjective _objective;
    private ObjectiveType lastType;

    public Objective()
    {
        type = ObjectiveType.reachXLength;
        Update();
    }

    public BaseObjective GetObjective()
    {
        return _objective;
    }

    public void Update()
    {
        if (lastType!=type)
        {
            switch (type)
            {
                case ObjectiveType.eatXFoodNTimes:
                    _objective = new EatXFoodNTimes();
                    lastType = ObjectiveType.eatXFoodNTimes;
                    break;
                
                case ObjectiveType.reachXLength:
                    _objective = new ReachXLength();
                    lastType = ObjectiveType.reachXLength;
                    break;
                
                case ObjectiveType.eatXPowerUpNTimes:
                    _objective = new EatXPowerUpNTimes();
                    lastType = ObjectiveType.eatXPowerUpNTimes;
                    break;
                
                case ObjectiveType.StayAliveForXTime:
                    _objective = new StayAliveForXTime();
                    lastType = ObjectiveType.StayAliveForXTime;
                    break;
            }
        }
        
    }

    
}
