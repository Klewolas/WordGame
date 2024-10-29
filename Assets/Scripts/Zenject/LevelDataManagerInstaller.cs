using Zenject;

public class LevelDataManagerInstaller : Installer<LevelDataManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<LevelDataManager>().AsSingle().NonLazy();
    }
}