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
    
    private LevelReferenceHolder _levelReferenceHolder;
    
    [Inject]
    void Construct(PlayerDataManager playerDataManager, LevelReferenceHolder levelReferenceHolder)
    {
        _playerDataManager = playerDataManager;
        _levelReferenceHolder = levelReferenceHolder;
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

        for (int i = 0; i < _levelReferenceHolder.LevelList.Count; ++i) {
            if(_levelReferenceHolder.LevelList[i].LevelNumber <= _playerDataManager.PlayerData.LastOpenedLevel)
                data.Add(new LevelItemData(_levelReferenceHolder.LevelList[i].LevelNumber));
        }
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex) {
        var child = item as LevelItem;
        child.LevelData = data[rowIndex];
    }
    
}
