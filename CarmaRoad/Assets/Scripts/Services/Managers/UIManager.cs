using System;
using UnityEngine;

namespace CarmaRoad
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        [SerializeField] private UI.KarmaBar karmaBar;
        [SerializeField] private UI.GameOver gameoverPanel;
        [SerializeField] private UI.StartMenu startMenuPanel;
        [SerializeField] private UI.SelectionPanel selectionPanel;
        [SerializeField] private RectTransform overlayButtons;

        private bool overlayKeySwitch;

        // Calling from here, as UIManager is singleton and GameManager is not a singleton
        public Action OverlayBtnSwitch;
        public Action RestartGameCall;
        public Action GameOverCall;

        public Action PlayBtnCall;
        public Action<int> SelectVehicleLeftRight;
        public Action OnVehicleSelection;

        public Action<Enum.LevelDifficulty> SelectDifficultyLevel;
        public Action OnDifficultySelectedStartGame;

        private void OnEnable()
        {
            GameOverCall += EnableGameOverPanel;
            PlayBtnCall += DisableStartMenu;
            RestartGameCall += DisableGameOverPanel;
            OverlayBtnSwitch += SwitchOverlayKeys;
            OnDifficultySelectedStartGame += DisableSelectionPanel;
        }

        private void Start()
        {
            overlayKeySwitch = false;
            EnableDisableGameObject(overlayButtons.gameObject, false);
            EnableDisableGameObject(karmaBar.gameObject, false);
            EnableDisableGameObject(startMenuPanel.gameObject, true);
        }

        private void OnDisable()
        {
            GameOverCall -= EnableGameOverPanel;
            PlayBtnCall -= DisableStartMenu;
            RestartGameCall -= DisableGameOverPanel;
            OverlayBtnSwitch -= SwitchOverlayKeys;
            OnDifficultySelectedStartGame -= DisableSelectionPanel;
        }

        private void EnableDisableGameObject(GameObject gameObject, bool enableOrDisable)
        {
            gameObject.SetActive(enableOrDisable);
        }

        private void DisableStartMenu()
        {
            EnableDisableGameObject(startMenuPanel.gameObject, false);
            EnableDisableGameObject(karmaBar.gameObject, true);
            EnableDisableGameObject(selectionPanel.gameObject, true);
        }

        private void EnableStartMenu()
        {
            EnableDisableGameObject(startMenuPanel.gameObject, true);
            EnableDisableGameObject(karmaBar.gameObject, false);
        }

        private void EnableGameOverPanel()
        {
            EnableDisableGameObject(gameoverPanel.gameObject, true);
        }

        private void DisableGameOverPanel()
        {
            EnableDisableGameObject(gameoverPanel.gameObject, false);
        }

        private void SwitchOverlayKeys()
        {
            overlayKeySwitch = !overlayKeySwitch;
            EnableDisableGameObject(overlayButtons.gameObject, overlayKeySwitch);
        }

        private void DisableSelectionPanel()
        {
            EnableDisableGameObject(selectionPanel.gameObject, false);
        }
    }
}
