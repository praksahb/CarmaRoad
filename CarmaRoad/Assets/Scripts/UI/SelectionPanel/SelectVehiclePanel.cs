using UnityEngine;
using UnityEngine.UI;

namespace CarmaRoad
{
    public class SelectVehiclePanel : MonoBehaviour
    {
        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button selectButton;

        private int leftClickValue = -1;
        private int rightClickValue = 1;

        private void OnEnable()
        {
            leftButton.onClick.AddListener(LeftButtonClicked);
            rightButton.onClick.AddListener(RightButtonClicked);
            selectButton.onClick.AddListener(SelectVehicleClicked);
        }

        private void OnDisable()
        {
            leftButton.onClick.RemoveListener(LeftButtonClicked);
            rightButton.onClick.RemoveListener(RightButtonClicked);
            selectButton.onClick.RemoveListener(SelectVehicleClicked);
        }

        private void LeftButtonClicked()
        {
            UIManager.Instance.SelectVehicleLeftRight?.Invoke(leftClickValue);
        }

        private void RightButtonClicked()
        {

            UIManager.Instance.SelectVehicleLeftRight?.Invoke(rightClickValue);
        }

        private void SelectVehicleClicked()
        {
            UIManager.Instance.OnVehicleSelection?.Invoke();
        }
    }
}
