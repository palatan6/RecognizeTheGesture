using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Класс отвечает основу игры.
/// Сменяет раунд за раундом. Стартует и прекращает игровой процесс.
/// </summary>

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
