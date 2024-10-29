using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

struct SpawnData
{
    public List<string> words;
    public float spawnTime;
    public float wordVelocity;

    public SpawnData(List<string> words, float spawnTime, float wordVelocity)
    {
        this.words = words;
        this.spawnTime = spawnTime;
        this.wordVelocity = wordVelocity;
    }
}

public class LevelSpawnManager : MonoBehaviour
{
    private LevelDataManager _levelDataManager;
    private ComboManager _comboManager;
    private ScoreManager _scoreManager;
    private LevelStateManager _levelStateManager;
    private InputListener _inputListener;
    
    private SpawnData _spawnData;

    public List<Word> AliveWords;

    #region Pool Fields

    [SerializeField] private Word _wordObject;
    [SerializeField] private ParticleSystem _particle;
    private Vector3 _particleSpawnPosition;
    private Transform _wordsParent;

    public IObjectPool<Word> Pool;
    public IObjectPool<ParticleSystem> ParticlePool;

    #endregion

    [Inject]
    void Construct(LevelDataManager levelDataManager, ComboManager comboManager, ScoreManager scoreManager,
        LevelStateManager levelStateManager, InputListener inputListener)
    {
        _levelDataManager = levelDataManager;
        _comboManager = comboManager;
        _scoreManager = scoreManager;
        _levelStateManager = levelStateManager;
        _inputListener = inputListener;
    }

    #region Unity LifeCycle

    void Start()
    {
        StartCoroutine(PrepareSpawnData());

        _inputListener.WordMatched += WordMatchedActions;
        _levelStateManager.LevelLose += ClearAllWords;
        _levelStateManager.LevelWin += ClearAllWords;
        
        Pool = new ObjectPool<Word>(CreatePooledItem,OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, 10, 150);
        
        ParticlePool = new ObjectPool<ParticleSystem>(CreatePooledParticle, OnTakeFromParticlePool,
            OnReturnedToParticlePool, OnDestroyParticlePoolObject, false, 10, 150);

        StartCoroutine(SpawnWordsCor());
    }

    private void OnDestroy()
    {
        _inputListener.WordMatched -= WordMatchedActions;
        _levelStateManager.LevelLose -= ClearAllWords;
        _levelStateManager.LevelWin -= ClearAllWords;
    }
    
    #endregion

    private IEnumerator PrepareSpawnData()
    {
        yield return new WaitUntil(() => _levelDataManager.IsInitialized);
        _wordsParent = GameCanvasObjectReferenceHolder.Instance.WordsSpawnParent;
        GetSpawnData();
    }
    
    private void GetSpawnData()
    {
        _spawnData = new SpawnData(_levelDataManager.LevelData.Words,
            _levelDataManager.LevelData.SpawnTime,
            _levelDataManager.LevelData.WordVelocity);
    }

    private void WordMatchedActions(Word word)
    {
        _particleSpawnPosition = word.gameObject.transform.position;

        ParticlePool.Get();
        AliveWords.Remove(word);
        Pool.Release(word);
        
        _scoreManager.IncreaseScore();
        _comboManager.IncreaseCombo();
    }
    
    
    private IEnumerator SpawnWordsCor()
    {
        while (true)
        {
            if (CheckCanSpawn())
            {
                var tempWord = _spawnData.words[Random.Range(0, _spawnData.words.Count)];
                Debug.Log("Word : " + tempWord);
                var word = Pool.Get();
                AliveWords.Add(word);
                word.SetText(tempWord);
                word.wordVelocity = _spawnData.wordVelocity;
            }
            
            yield return new WaitForSeconds(_spawnData.spawnTime);
        }
    }

    private bool CheckCanSpawn()
    {
        return _levelStateManager.GameState == GameState.GamePlay && _spawnData.words != null;
    }


    private void ClearAllWords()
    {
        foreach (var word in AliveWords)
        {
            _particleSpawnPosition = word.gameObject.transform.position;

            ParticlePool.Get();
            Pool.Release(word);
        }
        AliveWords.Clear();
    }

    #region Pool Functions
    
    private Word CreatePooledItem()
    {
        var word = Instantiate(_wordObject, new Vector3(Random.Range(-2.5f,2.5f), 4, 0), Quaternion.identity, _wordsParent);

        return word;
    }
    
    private void OnTakeFromPool(Word obj)
    {
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 4, 0);
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
        var particle = Instantiate(_particle, _particleSpawnPosition, Quaternion.identity, _wordsParent);


        var returnToPool = particle.gameObject.AddComponent<ReturnToPool>();
        returnToPool.pool = ParticlePool;
        
        return particle;
    }
    
    private void OnTakeFromParticlePool(ParticleSystem obj)
    {
        obj.gameObject.SetActive(true);
        obj.gameObject.transform.position = _particleSpawnPosition;
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