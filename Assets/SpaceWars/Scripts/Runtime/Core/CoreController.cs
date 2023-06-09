using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Bootstrap;
using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Signals.Gameplay;
using SpaceWars.Runtime.Signals.Ui;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Core {
    public class CoreController : IInitializable {
        public int CurrentLevel { get; private set; }
        public int EnemiesDestroyed { get; private set; }
        public int EnemiesToDestroy { get; private set; }

        private SignalBus _signalBus;
        private ConfigsController _configsController;
        private SceneController _sceneController;

        [Inject]
        private CoreController(SignalBus signalBus,
            ConfigsController configsController,
            SceneController sceneController) {
            _signalBus = signalBus;
            _configsController = configsController;
            _sceneController = sceneController;
        }

        public void Initialize() {
            _signalBus.Subscribe<SignalUnitDestroyed>(OnEnemyDestroyed);
            _signalBus.Subscribe<SignalPause>(OnPause);
        }

        public async UniTask StartGameplay(bool nextLevel) {
            if (!nextLevel) {
                ResetProgress();
            }
            StartNextLevel();
            await _sceneController.LoadGameplayAsync();
        }

        public void ResetProgress() {
            CurrentLevel = 0;
        }

        public void StartNextLevel() {
            CurrentLevel++;
            EnemiesDestroyed = 0;
            EnemiesToDestroy = Mathf.FloorToInt(_configsController.DifficultData.EnemiesToDestroyBase
                + _configsController.DifficultData.EnemiesToDestroyMultiplier * CurrentLevel);

        }

        private void OnEnemyDestroyed(SignalUnitDestroyed signal) {
            EnemiesDestroyed++;
            if (EnemiesDestroyed >= EnemiesToDestroy) {
                _signalBus.Fire(new SignalLevelCompleted());
            }
        }

        public void OnPause() {
            Time.timeScale = Mathf.Approximately(Time.timeScale, 1f) ? 0f : 1f;
        }
    }
}