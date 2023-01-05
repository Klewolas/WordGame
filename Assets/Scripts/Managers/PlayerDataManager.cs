using System;
using System.IO;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData PlayerData;
    
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
        
        PlayerData = new PlayerData();
        _saveFile = Application.persistentDataPath + "/playerData.json";
        PlayerData = ReadFile();
    }

    private PlayerData ReadFile()
    {
        if (!File.Exists(_saveFile)) return PlayerData;

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