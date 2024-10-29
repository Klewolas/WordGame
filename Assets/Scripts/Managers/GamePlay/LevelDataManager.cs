using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataManager
{
    private PlayerDataManager _playerDataManager;
    
    public bool IsInitialized { get; private set; }
    public LevelData LevelData { get; private set; }

    public LevelDataManager(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
        
        GetLevelData();
    }
    
    void GetLevelData()
    {
        LevelData = Resources.Load<LevelData>("ScriptableObjects/Levels/Level" +
                                              _playerDataManager.PlayerData.CurrentLevel);
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