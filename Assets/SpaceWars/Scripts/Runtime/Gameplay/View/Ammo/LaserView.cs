using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Gameplay.Model.Shooting;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons;
using System;
using System.Threading;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.View.Ammo {
    public class LaserView : MonoBehaviour {
        [SerializeField] ShootingModel shootingModel;
        [SerializeField] GameObject laser;
        [SerializeField] float durationSeconds;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        private void Start() {
            shootingModel.OnFired += AnimateLaser;
        }

        private void OnEnable() {
            laser.SetActive(false);
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void AnimateLaser(IWeapon iWeapon) {
            if (iWeapon.WeaponType != WeaponType.Laser) {
                return;
            }

            if (laser == null) {
                return;
            }

            laser.SetActive(true);
            try {
                await UniTask.Delay(TimeSpan.FromSeconds(durationSeconds),
                    false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                laser.SetActive(false);
            } catch (OperationCanceledException) {
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}