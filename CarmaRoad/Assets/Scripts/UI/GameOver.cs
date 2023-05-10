using UnityEngine;
using UnityEngine.UI;

namespace CarmaRoad.UI
{
    public class GameOver : MonoBehaviour
    {
        private Button restartBtn;

        private void Awake()
        {
            restartBtn = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {

            if (restartBtn != null)
            {
                restartBtn.onClick.AddListener(ButtonClickHandler);
            }
        }

        private void OnDisable()
        {
            if (restartBtn != null)
            {
                restartBtn.onClick.RemoveListener(ButtonClickHandler);
            }
        }

        private void ButtonClickHandler()
        {
            // Restart the scene or something 

            // invoke an event or something.
            UIManager.Instance.RestartGameCall?.Invoke();
        }
    }
}
