using Zenject;

public class PlayerDataManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerDataManager>().AsSingle().NonLazy();
    }
}