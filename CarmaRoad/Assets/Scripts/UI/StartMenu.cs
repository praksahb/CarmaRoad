using UnityEngine;
using UnityEngine.UI;

namespace CarmaRoad.UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button switchOverlayKeys;
        [SerializeField] private TMPro.TextMeshProUGUI switchOnOff;

        private string onOffswap = "on";

        private void OnEnable()
        {
            playButton.onClick.AddListener(StartGame);
            switchOverlayKeys.onClick.AddListener(SwitchOverlayKeys);
        }

        private void StartGame()
        {
            // call to start the game.
            UIManager.Instance.PlayBtnCall?.Invoke();
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
