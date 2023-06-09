using Zenject;

namespace SpaceWars.Runtime.Core {
    public class CoreInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<CoreController>().AsSingle();
        }
    }
}