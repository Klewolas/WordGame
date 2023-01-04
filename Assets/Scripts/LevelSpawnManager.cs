using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

struct SpawnData
{
    public List<string> words;
    public int spawnTime;
    public float wordVelocity;

    public SpawnData(List<string> words, int spawnTime, float wordVelocity)
    {
        this.words = words;
        this.spawnTime = spawnTime;
        this.wordVelocity = wordVelocity;
    }
}

public class LevelSpawnManager : MonoBehaviour
{
    private static LevelSpawnManager _instance;
    public static LevelSpawnManager Instance => _instance;

    public float elapsedTime = 0.0f;

    private SpawnData _spawnData;

    public List<string> AliveWords;

    #region Pool Fields

    [SerializeField] private Word _wordObject;
    private IObjectPool<Word> _pool;

    #endregion

    #region Unity LifeCycle

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
        _spawnData = new SpawnData(new List<string>(), 999, 999);
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized);
        GetSpawnData();
        
        _pool = new ObjectPool<Word>(CreatePooledItem,OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 150);
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
            var word = _pool.Get();
            word.SetText(tempWord);
            word.wordVelocity = 0.0055f;
        }
    }

    #endregion
    
    private void GetSpawnData()
    {
        _spawnData.words = LevelDataManager.Instance.LevelData.Words;
        _spawnData.spawnTime = LevelDataManager.Instance.LevelData.SpawnTime;
        _spawnData.wordVelocity = LevelDataManager.Instance.LevelData.WordVelocity;
    }

    #region Pool Functions
    
    private Word CreatePooledItem()
    {
        var word = Instantiate(_wordObject);

        return word;
    }
    
    private void OnTakeFromPool(Word obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Word obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReturnedToPool(Word obj)
    {
        obj.gameObject.SetActive(false);
    }


    #endregion
}