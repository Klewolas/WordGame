using System;
using UnityEngine;

public class ScoreManager
{
    private ComboManager _comboManager;
    private int _playerScore;

    private int _earnScoreCount = 10;

    public event Action<int> ScoreIncreased;

    public ScoreManager(ComboManager comboManager)
    {
        _comboManager = comboManager;
        ResetPlayerScore();
    }

    public void IncreaseScore()
    {
        _playerScore += _earnScoreCount * _comboManager.ComboCount;
        ScoreIncreased?.Invoke(_playerScore);
    }

    private void ResetPlayerScore()
    {
        _playerScore = 0;
    }
    
}
