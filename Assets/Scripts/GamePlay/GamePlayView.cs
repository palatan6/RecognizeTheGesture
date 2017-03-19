using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayView : AbstractGamePlayView
{
    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private TimeSpan _countDown;

    [SerializeField]
    private float _showTime = 0.7f;

    public override TimeSpan CountDownTimer
    {
        get
        {
            return _countDown;
        }
        set
        {
            _countDown = value;

            _timerText.text = String.Format("{0:00}:{1:00}", _countDown.Seconds, _countDown.Milliseconds);
        }
    }

    [SerializeField]
    private GameObject _rightSign;

    [SerializeField]
    private GameObject _wrongSign;

    [SerializeField]
    private Text[] _scoreCounterTexts;

    [SerializeField]
    private GameObject _startWindow;
    [SerializeField]
    private GameObject _mainWindow;
    [SerializeField]
    private GameObject _levelEndWindow;

    public override int TotalScoreCounter
    {
        set
        {
            for (int i = 0; i < _scoreCounterTexts.Length; ++i)
            {
                _scoreCounterTexts[i].text = value.ToString();
            }
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGameOver()
    {
        _mainWindow.SetActive(false);
        _levelEndWindow.SetActive(true);
    }

    public override void OnRoundWon(bool isWon)
    {
        _rightSign.SetActive(false);
        _wrongSign.SetActive(false);

        StartCoroutine(ShowSign(isWon ? _rightSign : _wrongSign));
    }

    IEnumerator ShowSign(GameObject sign)
    {
        sign.SetActive(true);
        yield return new WaitForSeconds(_showTime);
        sign.SetActive(false);
    }

    public void OnStartButtonClick() //invoked in UI button
    {
        _startWindow.SetActive(false);
        _mainWindow.SetActive(true);
        _levelEndWindow.SetActive(false);

        OnStartGame();
    }
}
