using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ReachXLength : BaseObjective
{
    [SerializeField] private int length;

    private SnakeController snake;
    public override void ResetObjective()
    {
        complete = false;
    }

    public override string Describe()
    {
        return "Reach " + length + " pieces of snake length : " + snake.GetLength() + "/" + length;
    }

    public override void Subscribe()
    {
        snake = SnakeController.Instance;
        snake.Grow += Checklength;
    }

    public override void Unsubscribe()
    {
        snake.Grow -= Checklength;
    }

    public void Checklength(int _length)
    {
        if (snake.GetLength()<length)
        {
            return;
        }
        UpdateStatus();
    }

    public override void UpdateStatus()
    {
        if (!GetStatus())
        {
            Complete();
        }
    }
}
