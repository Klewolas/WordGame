using Zenject;

public class GamePlayManagersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        ComboManagerInstaller.Install(Container);
        LevelDataManagerInstaller.Install(Container);
        ScoreManagerInstaller.Install(Container);
        LifeManagerInstaller.Install(Container);
        LevelStateManagerInstaller.Install(Container);
    }
}