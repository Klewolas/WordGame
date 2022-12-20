using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

struct SpawnData
{
    public List<string> words;
    public int spawnTime;

    public SpawnData(List<string> words, int spawnTime)
    {
        this.words = words;
        this.spawnTime = spawnTime;
    }
}

public class LevelSpawnManager : MonoBehaviour
{
    private static LevelSpawnManager _instance;
    public static LevelSpawnManager Instance => _instance;
    
    public float elapsedTime = 0.0f;

    private SpawnData _spawnData;

    public List<string> AliveWords;
    
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

    IEnumerator Start()
    {
        _spawnData = new SpawnData(new List<string>(), 999);
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized); 
        GetSpawnData();
    }

    private void GetSpawnData()
    {
        _spawnData.words = LevelDataManager.Instance.LevelData.Words;
        _spawnData.spawnTime = LevelDataManager.Instance.LevelData.SpawnTime;
    }

    void LateUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > _spawnData.spawnTime)
        {
            elapsedTime = 0;
            var tempWord = _spawnData.words[Random.Range(0, _spawnData.words.Count)];
            Debug.Log("Word : " + tempWord);
            AliveWords.Add(tempWord);
        }
    }
}