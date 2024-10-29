using Zenject;

public class ScoreManagerInstaller : Installer<ScoreManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ScoreManager>().AsSingle().NonLazy();
    }
}
