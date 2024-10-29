    using UnityEngine;
    using Zenject;

    public class InputListenerInstaller : MonoInstaller
    {
        [SerializeField] private InputListener _inputListener;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputListener).AsSingle().NonLazy();
        }
    }
