using System;
using UnityEngine;

namespace CarmaRoad
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        [SerializeField] private UI.KarmaBar karmaBar;
        [SerializeField] private UI.GameOver gameoverPanel;
        [SerializeField] private UI.StartMenu startMenuPanel;


        // Calling from here, as UIManager is singleton and GameManager is not a singleton
        public Action GameOverCall;
        public Action StartGameCall;
        public Action RestartGameCall;

        private void OnEnable()
        {
            GameOverCall += EnableGameOverPanel;
            StartGameCall += DisableStartMenu;
            RestartGameCall += DisableGameOverPanel;
        }
        private void OnDisable()
        {
            GameOverCall -= EnableGameOverPanel;
            StartGameCall -= DisableStartMenu;
            RestartGameCall -= DisableGameOverPanel;
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
}
