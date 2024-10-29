using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : RecyclingListViewItem
{
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

    private void Start()
    {
        _levelButton.onClick.AddListener(SetCurrentLevel);
    }

    private void SetCurrentLevel()
    {
        PlayerDataManager.Instance.PlayerData.CurrentLevel = levelData.level;
    }
}
