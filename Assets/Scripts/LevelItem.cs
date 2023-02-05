using TMPro;
using UnityEngine;

public class LevelItem : RecyclingListViewItem
{
    [SerializeField] private TMP_Text _levelText;

    private LevelItemData levelData;
    public LevelItemData LevelData {
        get { return levelData; }
        set {
            levelData = value;
            _levelText.text = "Level : " + levelData.level.ToString();
        }
    }
}
