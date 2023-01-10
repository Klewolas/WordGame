using System;
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

    private float elapsedTime = 0.0f;

    private SpawnData _spawnData;

    public List<Word> AliveWords;

    [SerializeField] private Transform _wordsParent;

    #region Pool Fields

    [SerializeField] private Word _wordObject;
    [SerializeField] private ParticleSystem _particle;
    public Vector3 ParticleSpawnPosition;
    
    public IObjectPool<Word> Pool;
    public IObjectPool<ParticleSystem> ParticlePool;

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

    void Start()
    {
        StartCoroutine(PrepareSpawnData());

        InputListener.WordMatched += WordMatchedActions;
        
        Pool = new ObjectPool<Word>(CreatePooledItem,OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 150);
        
        ParticlePool = new ObjectPool<ParticleSystem>(CreatePooledParticle, OnTakeFromParticlePool,
            OnReturnedToParticlePool, OnDestroyParticlePoolObject, false, 10, 150);
    }

    private void OnDestroy()
    {
        InputListener.WordMatched -= WordMatchedActions;
    }

    void LateUpdate()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > _spawnData.spawnTime)
        {
            elapsedTime = 0;
            var tempWord = _spawnData.words[Random.Range(0, _spawnData.words.Count)];
            Debug.Log("Word : " + tempWord);
            var word = Pool.Get();
            AliveWords.Add(word);
            word.SetText(tempWord);
            word.wordVelocity = _spawnData.wordVelocity;
        }
    }

    #endregion

    private IEnumerator PrepareSpawnData()
    {
        _spawnData = new SpawnData(new List<string>(), 999, 999);
        yield return new WaitUntil(() => LevelDataManager.Instance.IsInitialized);
        GetSpawnData();
    }
    
    private void GetSpawnData()
    {
        _spawnData.words = LevelDataManager.Instance.LevelData.Words;
        _spawnData.spawnTime = LevelDataManager.Instance.LevelData.SpawnTime;
        _spawnData.wordVelocity = LevelDataManager.Instance.LevelData.WordVelocity;
    }

    private void WordMatchedActions(Word word)
    {
        ScoreManager.Instance.IncreaseScore();
        ComboManager.Instance.IncreaseCombo();
        
        ParticleSpawnPosition = word.gameObject.transform.position;

        ParticlePool.Get();
        AliveWords.Remove(word);
        Pool.Release(word);
    }

    #region Pool Functions
    
    private Word CreatePooledItem()
    {
        var word = Instantiate(_wordObject, new Vector3(Random.Range(-3f,3f), 4, 0), Quaternion.identity, _wordsParent);

        return word;
    }
    
    private void OnTakeFromPool(Word obj)
    {
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = new Vector3(Random.Range(-3f, 3f), 4, 0);
    }

    private void OnDestroyPoolObject(Word obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReturnedToPool(Word obj)
    {
        obj.gameObject.SetActive(false);
    }
    
    private ParticleSystem CreatePooledParticle()
    {
        var particle = Instantiate(_particle, ParticleSpawnPosition, Quaternion.identity, _wordsParent);


        var returnToPool = particle.gameObject.AddComponent<ReturnToPool>();
        returnToPool.pool = ParticlePool;
        
        return particle;
    }
    
    private void OnTakeFromParticlePool(ParticleSystem obj)
    {
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = ParticleSpawnPosition;
    }

    private void OnDestroyParticlePoolObject(ParticleSystem obj)
    {
        Destroy(obj.gameObject);
    }

    private void OnReturnedToParticlePool(ParticleSystem obj)
    {
        obj.gameObject.SetActive(false);
    }


    #endregion
}