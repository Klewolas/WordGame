using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public bool IsInitialized { get; private set;}
    public LevelData LevelData { get; private set; }

    private static LevelDataManager _instance;
    public static LevelDataManager Instance => _instance;
    
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
    
    void GetLevelData()
    {
        LevelData = Resources.Load<LevelData>("ScriptableObjects/Levels/Level" +
                                                   (PlayerDataManager.Instance.PlayerData.CurrentLevel + 1));

        if (LevelData == null)
        {
            LevelData = Resources.Load<LevelData>("ScriptableObjects/Levels/DefaultLevel");
        }
        Debug.Log("LevelDataManager | Level data received.");

        IsInitialized = true;
    }
}
