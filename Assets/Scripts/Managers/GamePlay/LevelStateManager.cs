using System;
using System.Collections;
using UnityEngine;
using Zenject;

struct LevelConditions
{
    public int levelWinScore;
    public int life;

    public LevelConditions(int levelWinScore, int life)
    {
        this.levelWinScore = levelWinScore;
        this.life = life;
    }
}

public class LevelStateManager : MonoBehaviour
{
    private LevelDataManager _levelDataManager;
    private LifeManager _lifeManager;
    private ScoreManager _scoreManager;
    
    public event Action LevelWin;

    public event Action LevelLose;

    private LevelConditions _levelConditions;

    public GameState GameState { get; private set; }

    [Inject]
    void Construct(LevelDataManager levelDataManager, LifeManager lifeManager, ScoreManager scoreManager)
    {
        _levelDataManager = levelDataManager;
        _lifeManager = lifeManager;
        _scoreManager = scoreManager;
    }
    
    private void Start()
    {
        StartCoroutine(PrepareLevelConditionsData());

        GameState = GameState.GamePlay;
        _scoreManager.ScoreIncreased += CheckScoreEnoughToWin;
        _lifeManager.LoseLife += CheckIsLifeZero;
    }

    private void OnDestroy()
    {
        _scoreManager.ScoreIncreased -= CheckScoreEnoughToWin;
        _lifeManager.LoseLife -= CheckIsLifeZero;
    }

    private IEnumerator PrepareLevelConditionsData()
    {
        _levelConditions = new LevelConditions(100, 100);
        yield return new WaitUntil(() => _levelDataManager.IsInitialized);
        GetLevelConditionsData();
    }

    private void GetLevelConditionsData()
    {
        _levelConditions.life = _levelDataManager.LevelData.Life;
        _levelConditions.levelWinScore = _levelDataManager.LevelData.LevelWinScore;
    }

    private void CheckScoreEnoughToWin(int score)
    {
        if (_levelConditions.levelWinScore < score)
        {
            GameState = GameState.GameWin;
            LevelWin?.Invoke();
        }
    }

    public void CheckIsLifeZero(int spentLife)
    {
        if (_levelConditions.life == spentLife)
        {
            GameState = GameState.GameLose;
            LevelLose?.Invoke();
        }
    }
}


public enum GameState
{
    GamePlay,
    GameWin,
    GameLose
}