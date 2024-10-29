    using UnityEngine;
    using Zenject;

    public class LevelStateManagerInstaller : Installer<LevelStateManagerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelStateManager>().AsSingle().NonLazy();
        }
    }
