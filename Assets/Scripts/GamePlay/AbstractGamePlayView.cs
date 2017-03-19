using System;
using UnityEngine;

public abstract class AbstractGamePlayView : MonoBehaviour
{
    public event Action StartGame;

    protected void OnStartGame()
    {
        if (StartGame != null)
        {
            StartGame();
        }    
    }

    public abstract TimeSpan CountDownTimer { get; set; }

    public abstract void OnGameOver();

    public abstract void OnRoundWon(bool isWon);

    public abstract int TotalScoreCounter { set; }
}
