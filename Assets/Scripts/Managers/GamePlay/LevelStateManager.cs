using System;
using System.Collections;
using UnityEngine;

struct LevelConditions
{
    public int levelWinScore;
    public float levelTime;

    public LevelConditions(int levelWinScore, float levelTime)
    {
        this.levelWinScore = levelWinScore;
        this.levelTime = levelTime;
    }
}

public class LevelStateManager : MonoBehaviour
{
    private static LevelStateManager _instance;
    public static LevelStateManager Instance => _instance;

    public static event Action LevelWin;

    public static event Action LevelLose;

    public static event Action<int> TimeUpdate;

    private LevelConditions _levelConditions;

    private bool _timerIsRunning;

    public GameState GameState { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(PrepareLevelConditionsData());

        GameState = GameState.GamePlay;
        ScoreManager.ScoreIncreased += CheckScoreEnoughToWin;

        _timerIsRunning = true;
    }

    private void LateUpdate()
    {
        if (_timerIsRunning == false && GameState != GameState.GamePlay)
        {
            return;
        }

        _levelConditions.levelTime -= Time.deltaTime;


        if (_levelConditions.levelTime < 0)
        {
            _timerIsRunning = false;
            GameState = GameState.GameLose;
            LevelLose?.Invoke();
        }

        TimeUpdate?.Invoke((int) _levelConditions.levelTime);
    }

    private IEnumerator PrepareLevelConditionsData()
    {
        _levelConditions = new LevelConditions(100, 100);
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized);
        GetLevelConditionsData();
    }

    private void GetLevelConditionsData()
    {
        _levelConditions.levelTime = LevelDataManager.Instance.LevelData.LevelTime;
        _levelConditions.levelWinScore = LevelDataManager.Instance.LevelData.LevelWinScore;
    }

    private void CheckScoreEnoughToWin(int score)
    {
        if (_levelConditions.levelWinScore < score)
        {
            _timerIsRunning = false;
            GameState = GameState.GameWin;
            LevelWin?.Invoke();
        }
    }
}


public enum GameState
{
    GamePlay,
    GameWin,
    GameLose
}