    using UnityEngine;
    using Zenject;

    public class LevelStateManagerInstaller : MonoInstaller
    {
        [SerializeField] private LevelStateManager _levelStateManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelStateManager).AsSingle().NonLazy();
        }
    }
