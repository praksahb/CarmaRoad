using UnityEngine;

namespace CarmaRoad.UI
{
    public class SelectionPanel : MonoBehaviour
    {
        [SerializeField] private SelectVehiclePanel selectVehiclePanel;
        [SerializeField] private SelectDifficultyPanel selectDifficultyPanel;

        private void OnEnable()
        {
            UIManager.Instance.OnVehicleSelection += OnVehicleSelected;
            UIManager.Instance.OnDifficultySelectedStartGame += OnDifficultySelected;

            selectVehiclePanel.gameObject.SetActive(true);
            selectDifficultyPanel.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            UIManager.Instance.OnVehicleSelection -= OnVehicleSelected;
            UIManager.Instance.OnDifficultySelectedStartGame -= OnDifficultySelected;

        }

        private void OnVehicleSelected()
        {
            selectVehiclePanel.gameObject.SetActive(false);
            // set difficulty selection true
            selectDifficultyPanel.gameObject.SetActive(true);
        }

        private void OnDifficultySelected()
        {
            selectDifficultyPanel.gameObject.SetActive(false);
        }
    }
}
