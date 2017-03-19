using System;
using UnityEngine;
using System.Collections;

public class GameplayModel : AbstarctGamePlayModel
{
    [SerializeField]
    private float _startRoundTime = 1500;

    [SerializeField]
    private float _everyRoundTimeMinus = 100;

    public override float EveryRoundTimeMinus
    {
        get { return _everyRoundTimeMinus; }
    }

    public override void OnRoundWon(bool isWon)
    {
        if (isWon)
        {
            ++ScorePointsCounter;
        }

        SendRoundWon(isWon);
    }

    public override void StartFirstRound()
    {
        ScorePointsCounter = 0;

        OnMainGameStarted(TimeSpan.FromMilliseconds(_startRoundTime));
    }

    public override void StartMainGame()
    {
        OnGameStart();
    }
}
