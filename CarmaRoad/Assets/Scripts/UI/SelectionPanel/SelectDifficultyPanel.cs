using UnityEngine;
using UnityEngine.UI;

namespace CarmaRoad
{
    public class SelectDifficultyPanel : MonoBehaviour
    {
        [SerializeField] private Button easyButton;
        [SerializeField] private Button mediumButton;
        [SerializeField] private Button hardButton;
        [SerializeField] private Button impossibleButton;

        private void OnEnable()
        {
            easyButton.onClick.AddListener(EasyButtonClicked);
            mediumButton.onClick.AddListener(MediumButtonClicked);
            hardButton.onClick.AddListener(HardButtonClicked);
            impossibleButton.onClick.AddListener(ImpossibleButtonClicked);
        }

        private void OnDisable()
        {
            easyButton.onClick.RemoveAllListeners();
            mediumButton.onClick.RemoveAllListeners();
            hardButton.onClick.RemoveAllListeners();
            impossibleButton.onClick.RemoveAllListeners();
        }

        private void EasyButtonClicked()
        {
            UIManager.Instance.SelectDifficultyLevel?.Invoke(Enum.LevelDifficulty.Easy);
        }
        private void MediumButtonClicked()
        {
            UIManager.Instance.SelectDifficultyLevel?.Invoke(Enum.LevelDifficulty.Medium);
        }
        private void HardButtonClicked()
        {
            UIManager.Instance.SelectDifficultyLevel?.Invoke(Enum.LevelDifficulty.Hard);
        }
        private void ImpossibleButtonClicked()
        {
            UIManager.Instance.SelectDifficultyLevel?.Invoke(Enum.LevelDifficulty.Impossible);
        }


    }
}
