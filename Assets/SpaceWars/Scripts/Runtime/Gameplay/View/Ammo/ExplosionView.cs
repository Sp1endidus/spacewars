using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo;
using System;
using System.Threading;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.View.Ammo {
    public class ExplosionView : MonoBehaviour {
        [SerializeField] private MissleModel missle;
        [SerializeField] private GameObject explosion;
        [SerializeField] private float explosionDurationSeconds;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        private void Start() {
            missle.OnExploded += AnimateExplosion;
            explosion.transform.localScale *= missle.Data.Radius * 2f;
        }

        private void OnEnable() {
            explosion.SetActive(false);
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void AnimateExplosion() {
            explosion.SetActive(true);
            try {
                await UniTask.Delay(TimeSpan.FromSeconds(explosionDurationSeconds),
                    false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                explosion.SetActive(false);
            } catch (OperationCanceledException) {
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}