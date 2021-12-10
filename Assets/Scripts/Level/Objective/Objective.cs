using System;
using UnityEngine;

public enum ObjectiveType
{
    //None,
    eatXFoodNTimes,
    reachXLength,
    gatherXPowerUpNTimes
}



[Serializable]
public class Objective 
{
    
    [SerializeField] private ObjectiveType type;
    [SerializeReference] private BaseObjective _objective;
    private ObjectiveType lasType;

    public Objective()
    {
        type = ObjectiveType.reachXLength;
        Update();
    }

    public void Update()
    {
        if (lasType!=type)
        {
            switch (type)
            {
                /*case ObjectiveType.None:
                    _objective = new BaseObjective();
                    lasType = ObjectiveType.None;
                    break;*/
                
                case ObjectiveType.eatXFoodNTimes:
                    _objective = new EatXFoodNTimes();
                    lasType = ObjectiveType.eatXFoodNTimes;
                    break;
                
                case ObjectiveType.reachXLength:
                    _objective = new ReachXLength();
                    lasType = ObjectiveType.reachXLength;
                    break;
            }
        }
        
    }

    
}
