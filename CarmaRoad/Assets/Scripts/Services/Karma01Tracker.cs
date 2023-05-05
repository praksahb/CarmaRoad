using System;
using UnityEngine;

public class Karma01Tracker : GenericMonoSingleton<Karma01Tracker>
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
}
