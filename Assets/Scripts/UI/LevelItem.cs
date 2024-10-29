using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelItem : RecyclingListViewItem
{
    private PlayerDataManager _playerDataManager;
    
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Button _levelButton;

    private LevelItemData levelData;
    public LevelItemData LevelData {
        get { return levelData; }
        set {
            levelData = value;
            _levelText.text = "Level : " + levelData.level.ToString();
        }
    }
    
    [Inject]
    void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }

    private void Start()
    {
        _levelButton.onClick.AddListener(SetCurrentLevel);
    }

    private void SetCurrentLevel()
    {
        _playerDataManager.PlayerData.CurrentLevel = levelData.level;
    }
}
