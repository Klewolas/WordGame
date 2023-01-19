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
        
        //TODO : Menude bulunduğu için her menu yüklendiğinde kendini dontdestroyonload'a ekliyor. MainMenu sahnesi yaratıp buradan başlatman lazım.
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        _saveFile = Application.persistentDataPath + "/playerData.json";
        PlayerData = ReadFile();
    }

    private void OnDestroy()
    {
        WriteFile();
    }

    private PlayerData ReadFile()
    {
        if (!File.Exists(_saveFile)) return PlayerData = new PlayerData(1);

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