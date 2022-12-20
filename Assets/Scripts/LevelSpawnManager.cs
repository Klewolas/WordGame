using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

struct SpawnData
{
    public List<string> words;
    public int spawnTime;
}

public class LevelSpawnManager : MonoBehaviour
{
    private static LevelSpawnManager _instance;
    public static LevelSpawnManager Instance => _instance;
    
    public float elapsedTime = 0.0f;

    private SpawnData _spawnData;
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

    private void Start()
    {
        GetSpawnData();
    }

    private void GetSpawnData()
    {
        _spawnData.words = LevelDataManager.Instance.LevelData.Words;
        _spawnData.spawnTime = LevelDataManager.Instance.LevelData.SpawnTime;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > _spawnData.spawnTime)
        {
            elapsedTime = 0;
            Debug.Log("Word : " + _spawnData.words[Random.Range(0, _spawnData.words.Count)]);
        }
    }
}