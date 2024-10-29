using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SO_LevelReferenceHolderInstaller", menuName = "ScriptableObjects/Level Reference Holder Installer", order = 0)]
public class LevelReferenceHolderInstaller : ScriptableObjectInstaller
{
    public LevelReferenceHolder levelReferenceHolder;

    public override void InstallBindings()
    {
        Container.BindInstance(levelReferenceHolder);
    }
}

[Serializable]
public class LevelReferenceHolder
{
    public List<LevelData> LevelList;

    public LevelData GetLevelData(int level)
    {
        try
        {
            return LevelList[level - 1];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}