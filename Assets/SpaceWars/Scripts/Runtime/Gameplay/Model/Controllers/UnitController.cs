using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Core;
using SpaceWars.Runtime.Gameplay.Model.Unit;
using SpaceWars.Runtime.Signals.Gameplay;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Controllers {
    public class UnitController : MonoBehaviour {
        [SerializeField] private Transform[] spawnPoints;

        private UnitModel.EnemyPool _enemyPool;
        private CoreController _coreController;
        private ConfigsController _configsController;
        private SignalBus _signalBus;
        private UnitModel _player;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        [Inject]
        private void Construct(UnitModel.EnemyPool enemyPool,
            CoreController coreController,
            ConfigsController configsController,
            SignalBus signalBus,
            UnitModel player) {
            _enemyPool = enemyPool;
            _coreController = coreController;
            _configsController = configsController;
            _signalBus = signalBus;
            _player = player;
        }

        private void OnEnable() {
            _signalBus.Subscribe<SignalUnitDestroyed>(OnUnitDestroyed);
        }

        private void OnDisable() {
            _signalBus.Unsubscribe<SignalUnitDestroyed>(OnUnitDestroyed);
            _cancellationTokenSource.Cancel();
        }

        private void Start() {
            _player.Initialize(_configsController.PlayerData);
            SpawnEnemiesAsync();
        }

        private async void SpawnEnemiesAsync() {
            var enemiesAmountBase = _configsController.DifficultData.EnemiesAmountBase;
            var enemiesAmount = Mathf.FloorToInt(enemiesAmountBase + (_coreController.CurrentLevel
                * _configsController.DifficultData.EnemiesAmountMultiplier));

            for (int i = 0; i < enemiesAmount; i++) {
                _enemyPool.Spawn(GetRandomSpawnPoint(), _configsController.EnemyData);
                try {
                    await UniTask.Delay(TimeSpan.FromSeconds(_configsController.DifficultData.DelayBetweenSpawnInSeconds),
                        false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                } catch (OperationCanceledException) {
                    return;
                }
            }
        }

        private Vector2 GetRandomSpawnPoint() {
            return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
        }

        private void OnUnitDestroyed(SignalUnitDestroyed signal) {
            var unitModel = signal.Unit.GetComponent<UnitModel>();
            if (unitModel.Data.IsPlayer) {
                _signalBus.Fire(new SignalGameover());
            } else {
                _enemyPool.Despawn(unitModel);
            }
        }
    }
}