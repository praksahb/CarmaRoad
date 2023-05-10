using System;
using UnityEngine;

namespace CarmaRoad
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        [SerializeField] private UI.KarmaBar karmaBar;
        [SerializeField] private UI.GameOver gameoverPanel;
        [SerializeField] private UI.StartMenu startMenuPanel;
        [SerializeField] private RectTransform overlayButtons;

<<<<<<< Updated upstream
        public Action GameOverCall;
        public Action StartGameCall;
=======
        private bool overlayKeySwitch;

        // Calling from here, as UIManager is singleton and GameManager is not a singleton
        public Action GameOverCall;
        public Action StartGameCall;
        public Action RestartGameCall;
        public Action OverlayBtnSwitch;
>>>>>>> Stashed changes

        private void OnEnable()
        {
            GameOverCall += EnableGameOverPanel;
            StartGameCall += DisableStartMenu;
<<<<<<< Updated upstream
=======
            RestartGameCall += DisableGameOverPanel;
            OverlayBtnSwitch += SwitchOverlayKeys;

            overlayKeySwitch = false;
>>>>>>> Stashed changes
        }
        private void OnDisable()
        {
            GameOverCall -= EnableGameOverPanel;
            StartGameCall -= DisableStartMenu;
<<<<<<< Updated upstream
=======
            RestartGameCall -= DisableGameOverPanel;
            OverlayBtnSwitch -= SwitchOverlayKeys;
        }

        private void DisableGameObject(GameObject gameObject, bool enableOrDisable)
        {
            gameObject.SetActive(enableOrDisable);
>>>>>>> Stashed changes
        }

        private void DisableStartMenu()
        {
            DisableGameObject(startMenuPanel.gameObject, false);
            DisableGameObject(karmaBar.gameObject, true);
        }

        private void EnableStartMenu()
        {
            DisableGameObject(startMenuPanel.gameObject, true);
            DisableGameObject(karmaBar.gameObject, false);
        }

        private void EnableGameOverPanel()
        {
            DisableGameObject(gameoverPanel.gameObject, true);
        }

        private void DisableGameOverPanel()
        {
            DisableGameObject(gameoverPanel.gameObject, false);
        }

        private void SwitchOverlayKeys()
        {
            overlayKeySwitch = !overlayKeySwitch;
            DisableGameObject(overlayButtons.gameObject, overlayKeySwitch);
        }
    }
}
