using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject levelFinishedPanel;

    private void OnEnable()
    {
        HidePausePanel();
        HideLevelFinishedPanel();
        LevelManager.Pause += ShowPausePanel;
        LevelManager.Continue += HidePausePanel;
        LevelManager.Finish += ShowLevelFinishedPanel;
        LevelManager.Starting += HideLevelFinishedPanel;

    }

    private void OnDisable()
    {
        LevelManager.Pause -= ShowPausePanel;
        LevelManager.Continue -= HidePausePanel;
        LevelManager.Finish -= ShowLevelFinishedPanel;
        LevelManager.Starting -= HideLevelFinishedPanel;
        
    }

    void ShowLevelFinishedPanel()
    {
        levelFinishedPanel.SetActive(true);
    }

    void HideLevelFinishedPanel()
    {
        levelFinishedPanel.SetActive(false);
    }

    void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
}
