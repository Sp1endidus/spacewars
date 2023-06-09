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
    public class BulletModel : AmmoBase {
        public class Pool : AmmoPool.Pool {

        }

        public override AmmoType AmmoType => AmmoType.Bullet;

        [SerializeField] private MovementModel movementModel;

        public BulletData Data { get; private set; }
        private AmmoPool _ammoPool;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        private bool _damageDealt;

        [Inject]
        private void Construct(ConfigsController configsController,
            AmmoPool ammoPool) {
            Data = configsController.BulletData;
            _ammoPool = ammoPool;
        }

        private void Start() {
            movementModel.Initialize(Data.MovementData);
            movementModel.SetInput(1f, 0f);
        }

        private void OnEnable() {
            _damageDealt = false;
            SelfDestroy();
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDestroy() {
            _cancellationTokenSource.Cancel();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!_damageDealt) {
                DealDamage(collision.gameObject);
            }
        }

        private void DealDamage(GameObject target) {
            var durability = target.GetComponent<DurabilityModel>();
            if (durability != null) {
                durability.DealDamage(Data.Damage);
            }

            _cancellationTokenSource.Cancel();
            _ammoPool.Despawn(this);
            _damageDealt = true;
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