using Zenject;

public class LifeManagerInstaller : Installer<LifeManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LifeManager>().AsSingle().NonLazy();
    }
}