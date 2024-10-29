using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class LevelSpawnManagerInstaller : MonoInstaller
{
    [SerializeField] private LevelSpawnManager _levelSpawnManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_levelSpawnManager).AsSingle().NonLazy();
    }
}