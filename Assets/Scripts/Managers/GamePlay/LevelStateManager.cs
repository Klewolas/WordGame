using System;
using Cysharp.Threading.Tasks;
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

public class LevelStateManager : IInitializable, IDisposable
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
    
    public void Initialize()
    {
        PrepareLevelConditionsData().Forget();

        GameState = GameState.GamePlay;
        _scoreManager.ScoreIncreased += CheckScoreEnoughToWin;
        _lifeManager.LoseLife += CheckIsLifeZero;
    }

    private async UniTask PrepareLevelConditionsData()
    {
        _levelConditions = new LevelConditions(100, 100);
        await UniTask.WaitUntil(() => _levelDataManager.IsInitialized);
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

    public void Dispose()
    {
        _scoreManager.ScoreIncreased -= CheckScoreEnoughToWin;
        _lifeManager.LoseLife -= CheckIsLifeZero;
    }
}


public enum GameState
{
    GamePlay,
    GameWin,
    GameLose
}