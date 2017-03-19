using UnityEngine;
using System.Collections;
using System;

public class GamePlayController : MonoBehaviour
{
    public AbstractGamePlayView gamePlayView;
    public AbstarctGamePlayModel gamePlayModel;

    private TimeSpan _currentRoundTime;
    private TimeSpan _currentCountDown;

    void OnEnable()
    {
        gamePlayModel.MainGameStarted += GamePlayModel_MainGameStarted;

        gamePlayModel.RoundWon += GamePlayModel_RoundWon;

        gamePlayModel.ScoreChanged += GamePlayModel_ScoreChanged;

        gamePlayView.StartGame += GamePlayView_StartGame;
    }

    void OnDisable() 
    {
        gamePlayModel.MainGameStarted -= GamePlayModel_MainGameStarted;

        gamePlayModel.ScoreChanged -= GamePlayModel_ScoreChanged;

        gamePlayModel.RoundWon -= GamePlayModel_RoundWon;

        gamePlayView.StartGame -= GamePlayView_StartGame;
    }

    private void GamePlayModel_MainGameStarted(TimeSpan newTimer)
    {
        _currentRoundTime =_currentCountDown = newTimer;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (_currentCountDown.TotalMilliseconds>0)
        {
            gamePlayView.CountDownTimer = _currentCountDown;

            yield return null;

            _currentCountDown = _currentCountDown.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
        }

        gamePlayView.OnGameOver();
        gamePlayModel.MainGameOver();
    }

    private void GamePlayModel_RoundWon(bool isWon)
    {
        gamePlayView.OnRoundWon(isWon);

        if (isWon)
        {
            _currentRoundTime = _currentRoundTime.Subtract(TimeSpan.FromMilliseconds(gamePlayModel.EveryRoundTimeMinus));

            _currentCountDown = _currentRoundTime;
        }
    }

    private void GamePlayModel_ScoreChanged(int count)
    {
        gamePlayView.TotalScoreCounter = count;
    }

    private void GamePlayView_StartGame()
    {
        gamePlayModel.StartMainGame();
    }
}
