using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Gameplay.Model.Durability;
using System;
using System.Threading;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.View.Durability {
    public class DurabilityView : MonoBehaviour {
        [SerializeField] DurabilityModel durabilityModel;
        [SerializeField] SpriteRenderer body;
        [SerializeField] float blinkDurationSeconds;

        private Color _originalColor;

        private CancellationTokenSource _cancellationTokenSource
            = new CancellationTokenSource();

        private void Awake() {
            _originalColor = body.color;
        }

        private void Start() {
            durabilityModel.OnDurabilityChanged += AnimateDamage;
        }

        private void OnEnable() {
            body.color = _originalColor;
        }

        private void OnDisable() {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void AnimateDamage(float durability) {
            body.color = Color.red;
            try {
                await UniTask.Delay(TimeSpan.FromSeconds(blinkDurationSeconds),
                    false, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                body.color = _originalColor;
            } catch (OperationCanceledException) {
                _cancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}