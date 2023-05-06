using UnityEngine;
using UnityEngine.UI;

namespace KarmaRoad
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button playGameButton;

        private void OnEnable()
        {
            playGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            // call to start the game.
            UIManager.Instance.StartGameCall?.Invoke();
        }
    }
}