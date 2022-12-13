using UnityEngine;

public class LevelSpawnManager : MonoBehaviour
{
    private static LevelSpawnManager _instance;
    public static LevelSpawnManager Instance => _instance;

    private SO_LevelWords _levelData;

    public float elapsedTime = 0.0f;

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
        GetLevelData();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > _levelData.SpawnTime)
        {
            elapsedTime = 0;
            Debug.Log("Word : " + _levelData.Words[Random.Range(0, _levelData.Words.Count)]);
        }
    }

    void GetLevelData()
    {
        _levelData = Resources.Load<SO_LevelWords>("ScriptableObjects/Levels/Level" +
                                                   (PlayerDataManager.Instance.PlayerData.CurrentLevel + 1));

        if (_levelData == null)
        {
            _levelData = Resources.Load<SO_LevelWords>("ScriptableObjects/Levels/DefaultLevel");
        }
    }
}