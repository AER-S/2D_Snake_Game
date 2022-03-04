using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinishedPanelController : GamePanelController
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private RectTransform starsPanel;
    [SerializeField] private Image starPrefab;

    [SerializeField] private TextMeshProUGUI result;


    protected override void UpdatePanel()
    {
        levelIndex.text = LevelManager.Instance.GetLevelIndex().ToString("000");
        score.text = SnakeController.Instance.GetScore().ToString();
        int stars = 0;
        for (int i = 0; i < 3; i++)
        {
            BaseObjective objective = LevelManager.Instance.GetObjective(i);
            objective.UpdateStatus();
            if (objective.GetStatus())
            {
                Instantiate(starPrefab, starsPanel, true);
                stars++;
            }
        }

        if (stars <= 0)
        {
            result.text = "YOU LOSE";
            continueButton.interactable = false;
        }
        else
        {
            result.text = "YOU WIN";
        }
    }

    protected override void ContinueGame()
    {
        int nextIndex = LevelManager.Instance.GetLevelIndex()+1;
        //PlayerPrefs.SetInt("levelToLoad",nextIndex);
        LevelManager.Instance.SetLevelIndex(nextIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.Instance.InitializeLevel();
    }
}
