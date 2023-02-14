using System;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData PlayerData;
    
    public bool IsPlayerDataReady { get; private set; }
    
    string _saveFile;
    
    private static PlayerDataManager _instance;
    public static PlayerDataManager Instance => _instance;
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
        
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        _saveFile = Application.persistentDataPath + "/playerData.json";
        PreparePlayerData();
    }

    private void OnDestroy()
    {
        WriteFile();
    }

    private void PreparePlayerData()
    {
        PlayerData = ReadFile();
        IsPlayerDataReady = true;
    }

    private PlayerData ReadFile()
    {
        if (!File.Exists(_saveFile)) return PlayerData = new PlayerData(1, 1);

        string fileContents = File.ReadAllText(_saveFile);
        PlayerData = JsonUtility.FromJson<PlayerData>(fileContents);
        Debug.Log("PlayerDataManager | Succesfully file readed.");
        return PlayerData;
    }

    private void WriteFile()
    {
        string jsonString = JsonUtility.ToJson(PlayerData);
        File.WriteAllText(_saveFile, jsonString);
        Debug.Log("PlayerDataManager | Succesfully file writed.");
    }
}