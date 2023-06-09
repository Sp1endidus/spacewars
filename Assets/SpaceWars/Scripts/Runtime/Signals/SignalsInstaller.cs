using SpaceWars.Runtime.Signals.Gameplay;
using SpaceWars.Runtime.Signals.Ui;
using Zenject;

namespace SpaceWars.Runtime.Signals {
    public class SignalsInstaller : MonoInstaller {
        public override void InstallBindings() {
            SignalBusInstaller.Install(Container);
            DeclareGameplay();
            DeclareUi();
        }

        private void DeclareGameplay() {
            Container.DeclareSignal<SignalUnitDestroyed>();
            Container.DeclareSignal<SignalLevelCompleted>();
            Container.DeclareSignal<SignalGameover>();
            Container.DeclareSignal<SignalPlayerBinding>();
        }

        private void DeclareUi() {
            Container.DeclareSignal<SignalPause>();
        }
    }
}