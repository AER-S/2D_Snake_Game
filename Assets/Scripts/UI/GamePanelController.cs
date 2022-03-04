using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class GamePanelController : MonoBehaviour
{
    [SerializeField] protected Button continueButton;
    [SerializeField] protected Button restartButton;
    [SerializeField] protected Button exitButton;
    
    [SerializeField] protected TextMeshProUGUI levelIndex;

    private void OnEnable()
    {
        continueButton.onClick.AddListener(ContinueGame);
        UpdatePanel();
    }

    protected abstract void UpdatePanel();

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(ContinueGame);
    }

    protected abstract void ContinueGame();

}
