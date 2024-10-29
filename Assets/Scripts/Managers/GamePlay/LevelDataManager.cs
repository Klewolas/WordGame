using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataManager
{
    private PlayerDataManager _playerDataManager;
    private LevelReferenceHolder _levelReferenceHolder;
    
    public bool IsInitialized { get; private set; }
    public LevelData LevelData { get; private set; }

    public LevelDataManager(PlayerDataManager playerDataManager, LevelReferenceHolder levelReferenceHolder)
    {
        _playerDataManager = playerDataManager;
        _levelReferenceHolder = levelReferenceHolder;
        
        GetLevelData();
    }
    
    void GetLevelData()
    {
        LevelData = _levelReferenceHolder.GetLevelData(_playerDataManager.PlayerData.CurrentLevel);
        
        if (LevelData == null)
        {
            Debug.LogError($"LevelDataManager | Level {_playerDataManager.PlayerData.CurrentLevel} is not found");
            SceneManager.LoadScene("Menu");
            return;
        }
        
        Debug.Log("LevelDataManager | Level data received.");

        IsInitialized = true;
    }
}