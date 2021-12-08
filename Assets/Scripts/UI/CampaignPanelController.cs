using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampaignPanelController : MonoBehaviour
{
    [SerializeField] private int levelsNumber=20;
    [SerializeField] private Button levelButtonPrefab;
    [SerializeField] private RectTransform buttonsContainer;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject levelSettingsPanel;
    private List<Button> levelsButtons = new List<Button>();

    private void OnEnable()
    {
        closeButton.onClick.AddListener(CloseCampaignPanel);
        StartButtons();
    }
    
    

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(CloseCampaignPanel);
        ClearButtons();
    }
    

    

    void StartButtons()
    {
        levelsButtons.Clear();
        for (int i = 1; i <= levelsNumber; i++)
        {
            Button levelButton = Instantiate(levelButtonPrefab, buttonsContainer);
            HandleLevelButton(levelButton,i);
            levelsButtons.Add(levelButton);
        }
    }

    void ClearButtons()
    {
        foreach (Button button in levelsButtons)
        {
            Destroy(button.gameObject);
        }
        levelsButtons.Clear();
    }
    void HandleLevelButton(Button _button, int _index)
    {
        TextMeshProUGUI buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = _index.ToString("00");
    }

    void CloseCampaignPanel()
    {
        lobbyPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
