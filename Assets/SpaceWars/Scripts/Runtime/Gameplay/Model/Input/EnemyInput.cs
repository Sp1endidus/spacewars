using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Gameplay.Model.Movement;
using SpaceWars.Runtime.Gameplay.Model.Shooting;
using SpaceWars.Runtime.Gameplay.Model.Unit;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Input {
    public class EnemyInput : MonoBehaviour {
        [SerializeField] private ShootingModel shootingModel;
        [SerializeField] private MovementModel movementModel;

        private UnitModel _player;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        [Inject]
        private void Construct(UnitModel player) {
            _player = player;
        }

        private void Update() {
            var direction = _player.transform.position - transform.position;
            float rotation = Mathf.Clamp(Vector2.SignedAngle(direction, transform.TransformDirection(Vector2.down)), -1f, 1f);
            float acceleration = Vector2.Distance(_player.transform.position, transform.position) > 8f ? 1f : 0f;
            movementModel.SetInput(acceleration, rotation);
        }

        private void OnEnable() {
            RandomShoot();
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDestroy() {
            _cancellationTokenSource.Cancel();
        }

        private async void RandomShoot() {
            try {
                while (true) {
                    await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.2f, 3f)),
                        false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    shootingModel.Fire();
                }
            } catch (OperationCanceledException) {
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}