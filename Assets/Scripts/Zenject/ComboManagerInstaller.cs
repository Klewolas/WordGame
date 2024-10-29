using Zenject;

public class ComboManagerInstaller : Installer<ComboManagerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ComboManager>().AsSingle().NonLazy();
    }
}