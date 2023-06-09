using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Configs.Ammo;
using SpaceWars.Runtime.Gameplay.Model.Durability;
using SpaceWars.Runtime.Gameplay.Model.Movement;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo {
    public class MissleModel : AmmoBase {
        public class Pool : AmmoPool.Pool {

        }

        public override AmmoType AmmoType => AmmoType.Missle;

        [SerializeField] private MovementModel movementModel;

        public MissleData Data { get; private set; }
        private AmmoPool _ammoPool;

        private Collider2D[] _buffer;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        public event Action OnExploded;

        [Inject]
        private void Construct(ConfigsController configsController,
            AmmoPool ammoPool) {
            Data = configsController.MissleData;
            _ammoPool = ammoPool;
            _buffer = new Collider2D[Data.BufferSize];
        }

        private void Start() {
            movementModel.Initialize(Data.MovementData);
            movementModel.SetInput(1f, 0f);
        }

        private void OnEnable() {
            SelfDestroy();
        }

        private void OnDestroy() {
            _cancellationTokenSource.Cancel();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Explode();
        }

        private async void Explode() {
            var count = Physics2D.OverlapCircleNonAlloc(transform.position, Data.Radius, _buffer);

            var targetsImpacted = 0;
            OnExploded?.Invoke();
            for (int i = 0; i < count; i++) {
                var durability = _buffer[i].GetComponent<DurabilityModel>();
                if (durability == null) {
                    continue;
                }

                durability.DealDamage(Data.Damage);
                await UniTask.WaitForEndOfFrame(this);
                if (!gameObject.activeSelf) {
                    return;
                }
                targetsImpacted++;
                if (targetsImpacted >= Data.MaxTargets) {
                    break;
                }
            }
            _cancellationTokenSource.Cancel();
            _ammoPool.Despawn(this);
        }

        private async void SelfDestroy() {
            try {
                await UniTask.Delay(TimeSpan.FromSeconds(Data.LifeTimeSeconds),
                    false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                _ammoPool.Despawn(this);
            } catch (OperationCanceledException) {
                _cancellationTokenSource = new CancellationTokenSource();
                return;
            }
        }
    }
}