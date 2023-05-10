using UnityEngine;
using UnityEngine.UI;

namespace CarmaRoad.UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button playGameButton;
        [SerializeField] private Button switchOverlayKeys;
        [SerializeField] private TMPro.TextMeshProUGUI switchOnOff;

        private string onOffswap = "on";

        private void OnEnable()
        {
            playGameButton.onClick.AddListener(StartGame);
            switchOverlayKeys.onClick.AddListener(SwitchOverlayKeys);
            Debug.Log("Sw: " + switchOnOff.text);
        }

        private void StartGame()
        {
            // call to start the game.
            UIManager.Instance.StartGameCall?.Invoke();
        }

        private void SwitchOverlayKeys()
        {
            UIManager.Instance.OverlayBtnSwitch?.Invoke();
            string temp = onOffswap;
            onOffswap = switchOnOff.text;
            switchOnOff.text = temp;
        }
    }
}
