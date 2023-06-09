using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Bootstrap {
    public class BootstrapInstaller : MonoInstaller {
        [SerializeField] private SceneController sceneController;

        public override void InstallBindings() {
            Container.Bind<SceneController>()
                .FromComponentInNewPrefab(sceneController)
                .AsSingle().NonLazy();
        }
    }
}