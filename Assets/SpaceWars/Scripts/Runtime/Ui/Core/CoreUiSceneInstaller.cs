using SpaceWars.Runtime.Ui.Core.Windows;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Ui.Core {
    public class CoreUiSceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindFactory<Object, WindowBase, WindowBase.Factory>()
                .FromFactory<PrefabFactory<WindowBase>>();
        }
    }
}