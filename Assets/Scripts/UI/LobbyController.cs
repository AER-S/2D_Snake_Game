using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button campaignButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        campaignButton.onClick.AddListener(GoToCampaignPanel);
        multiplayerButton.onClick.AddListener(GoToMultiplayerSettingsPanel);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        campaignButton.onClick.RemoveListener(GoToCampaignPanel);
        multiplayerButton.onClick.RemoveListener(GoToMultiplayerSettingsPanel);
        quitButton.onClick.RemoveListener(QuitGame);
    }
    
    public void GoToCampaignPanel(){}
    public void GoToMultiplayerSettingsPanel(){}

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
