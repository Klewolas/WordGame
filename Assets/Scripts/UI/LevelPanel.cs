using System.Collections.Generic;
using UnityEngine;
using Zenject;

public struct LevelItemData {
    public int level;

    public LevelItemData(int level)
    {
        this.level = level;
    }
}
public class LevelPanel : MonoBehaviour
{
    private PlayerDataManager _playerDataManager;
    
    public RecyclingListView theList;
    private List<LevelItemData> data = new List<LevelItemData>();
    
    [SerializeField] private LevelReferenceHolder _levelReferenceHolder;
    
    [Inject]
    void Construct(PlayerDataManager playerDataManager)
    {
        _playerDataManager = playerDataManager;
    }
    
    private void Start() {
        theList.ItemCallback = PopulateItem;

        RetrieveData();

        // This will resize the list and cause callbacks to PopulateItem for
        // items that are needed for the view
        theList.RowCount = data.Count;
    }

    private void RetrieveData() {
        data.Clear();

        for (int i = 0; i < _levelReferenceHolder.levelList.Count; ++i) {
            if(_levelReferenceHolder.levelList[i].LevelNumber <= _playerDataManager.PlayerData.LastOpenedLevel)
                data.Add(new LevelItemData(_levelReferenceHolder.levelList[i].LevelNumber));
        }
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex) {
        var child = item as LevelItem;
        child.LevelData = data[rowIndex];
    }
    
}
