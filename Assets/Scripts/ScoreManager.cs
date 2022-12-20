using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _playerScore;

    [SerializeField] private int _earnScoreCount;
    
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    public static event Action<int> ScoreIncreased;
    
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

    public void IncreaseScore()
    {
        _playerScore += _earnScoreCount;
        ScoreIncreased?.Invoke(_playerScore);
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
