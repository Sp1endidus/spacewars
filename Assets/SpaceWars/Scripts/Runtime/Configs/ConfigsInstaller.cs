using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Configs {
    public class ConfigsInstaller : MonoInstaller {
        [SerializeField] private ConfigsController configsController;

        public override void InstallBindings() {
            Container.Bind<ConfigsController>()
                .FromComponentInNewPrefab(configsController)
                .AsSingle().NonLazy();
        }
    }
}