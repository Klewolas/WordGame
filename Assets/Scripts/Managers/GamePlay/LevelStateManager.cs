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

    private LevelConditions _levelConditions;

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
            GameState = GameState.GameWin;
            LevelWin?.Invoke();
        }
    }

    public void LevelLoseWithTime()
    {
        GameState = GameState.GameLose;
        LevelLose?.Invoke();
    }
}


public enum GameState
{
    GamePlay,
    GameWin,
    GameLose
}