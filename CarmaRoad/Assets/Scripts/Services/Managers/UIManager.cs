using KarmaRoad;
using System;
using UnityEngine;

public class UIManager : GenericMonoSingleton<UIManager>
{
    [SerializeField] private KarmaBar karmaBar;
    [SerializeField] private GameOver gameoverPanel;
    [SerializeField] private StartMenu startMenuPanel;

    public Action GameOverCall;
    public Action StartGameCall;

    private void OnEnable()
    {
        GameOverCall += EnableGameOverPanel;
        StartGameCall += DisableStartMenu;
    }
    private void OnDisable()
    {
        GameOverCall -= EnableGameOverPanel;
        StartGameCall -= DisableStartMenu;
    }

    private void DisableStartMenu()
    {
        startMenuPanel.gameObject.SetActive(false);
        karmaBar.gameObject.SetActive(true);
    }

    private void EnableStartMenu()
    {
        startMenuPanel.gameObject.SetActive(true);
        karmaBar.gameObject.SetActive(false);
    }

    private void EnableGameOverPanel()
    {
        gameoverPanel.gameObject.SetActive(true);
    }

    private void DisableGameOverPanel()
    {
        gameoverPanel.gameObject.SetActive(false);
    }
}
