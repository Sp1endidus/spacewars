using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting {
    public class ShootingModel : MonoBehaviour {

        private IWeapon _currentWeapon;

        private WeaponPool _weaponPool;

        public event Action<WeaponType> OnWeaponSelected;
        public event Action<IWeapon> OnFired;

        private bool _isReloading;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        [Inject]
        private void Construct(WeaponPool weaponPool) {
            _weaponPool = weaponPool;
        }

        private void Start() {
            SwitchWeapon(WeaponType.Laser);
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Fire() {
            if (_isReloading) {
                return;
            }

            _currentWeapon.Fire();
            OnFired?.Invoke(_currentWeapon);
            Reload();
        }

        public void SwitchWeapon(WeaponType weaponType) {
            if (_currentWeapon != null) {
                if (_currentWeapon.WeaponType == weaponType) {
                    return;
                }

                _weaponPool.Despawn(_currentWeapon);
            }
            _currentWeapon = _weaponPool.Spawn(weaponType, transform);
            OnWeaponSelected?.Invoke(weaponType);
        }

        private async void Reload() {
            _isReloading = true;
            try {
                await UniTask.Delay(TimeSpan.FromSeconds(_currentWeapon.ReloadTime),
                    false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            } catch (OperationCanceledException) {

            }
            _isReloading = false;
        }
    }
}