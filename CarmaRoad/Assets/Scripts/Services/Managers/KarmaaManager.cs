using System;
using UnityEngine;
namespace CarmaRoad
{
    // numeric naming helps to allow it be above KarmaBar in script execution.
    // alternatively can name the class as KarmaaTracker or KarmaaManager then it will be created before KarmaBar
    public class KarmaaManager : GenericMonoSingleton<KarmaaManager>
    {
        [SerializeField] private int maxKarma;
        [SerializeField] private int karmaModifier;

        private int currentKarma;

        public int MaxKarma { get { return maxKarma; } }

        public Action<int> OnKarmaChanged;

        private void Start()
        {
            currentKarma = maxKarma;
        }

        private void OnEnable()
        {
            UIManager.Instance.RestartGameCall += RestartGameCalled;
        }

        private void OnDisable()
        {
            UIManager.Instance.RestartGameCall -= RestartGameCalled;
        }

        public void ReduceKarma()
        {
            currentKarma -= karmaModifier;

            // Make sure karma doesn't go below 0
            currentKarma = Mathf.Max(currentKarma, 0);

            // Invoke the OnKarmaChanged action
            OnKarmaChanged?.Invoke(currentKarma);

            // If karma is zero, end the game
            if (currentKarma <= 0)
            {
                EndGame();
            }
        }

        // game over logic? 
        private void EndGame()
        {
            UIManager.Instance.GameOverCall?.Invoke();
        }

        // restart game - reset currentKarma
        private void RestartGameCalled()
        {
            currentKarma = maxKarma;
            OnKarmaChanged?.Invoke(currentKarma);
        }
    }
}
