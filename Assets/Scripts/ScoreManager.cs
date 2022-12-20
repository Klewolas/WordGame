using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _playerScore;
    
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;
    
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

    public void IncreaseScore(int score)
    {
        _playerScore += score;
    }

    public void ResetPlayerScore()
    {
        _playerScore = 0;
    }

    public bool CheckScoreIsEnoughForGame()
    {
        return _playerScore > LevelDataManager.Instance.LevelData.LevelWinScore;
    }
}
