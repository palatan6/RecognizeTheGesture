using System;
using UnityEngine;

public abstract class AbstarctGamePlayModel : MonoBehaviour
{
    public abstract void OnRoundWon(bool isWon);

    public abstract void StartFirstRound();

    public event Action<TimeSpan> MainGameStarted;

    protected void OnMainGameStarted(TimeSpan newRoundTimer)
    {
        if (MainGameStarted!=null)
        {
            MainGameStarted(newRoundTimer);
        }
    }

    public event Action<bool> RoundWon;

    protected void SendRoundWon(bool isWon)
    {
        if (RoundWon!=null)
        {
            RoundWon(isWon);
        }
    }

    public abstract float EveryRoundTimeMinus { get; }

    public event Action<int> ScoreChanged;

    private int _scorePointsCounter;

    protected int ScorePointsCounter
    {
        get { return _scorePointsCounter; }

        set
        {
            _scorePointsCounter = value;
            if (ScoreChanged!=null)
            {
                ScoreChanged(_scorePointsCounter);
            }
        }
    }

    public event Action GameStart;

    protected void OnGameStart()
    {
        if (GameStart != null)
        {
            GameStart();
        }
    }

    public event Action GameOver;

    protected void OnGameOver()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }

    public abstract void StartMainGame();

    public virtual void MainGameOver()
    {
        OnGameOver();
    }
}
