using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausePanelController : GamePanelController
{
    
    [SerializeField] private TextMeshProUGUI[] objectives;

    protected override void UpdatePanel()
    {
        levelIndex.text = LevelManager.Instance.GetLevelIndex().ToString("000");
        for (int i = 0; i < 3; i++)
        {
            BaseObjective objective = LevelManager.Instance.GetObjective(i);
            objective.UpdateStatus();
            if (objective.GetStatus())
            {
                objectives[i].color = Color.green;
            }

            objectives[i].text = objective.Describe();
        }
    }

    protected override void ContinueGame()
    {
        LevelManager.Instance.ContinueGame();
    }
}
