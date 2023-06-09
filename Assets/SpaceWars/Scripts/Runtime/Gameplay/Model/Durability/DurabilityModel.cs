using SpaceWars.Runtime.Configs.Durability;
using SpaceWars.Runtime.Signals.Gameplay;
using System;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Durability {
    public class DurabilityModel : MonoBehaviour {
        private DurabilityData _data;

        private SignalBus _signalBus;

        public float Durability { get; private set; }

        public event Action<float> OnDurabilityChanged;

        [Inject]
        private void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
        }

        public void Initialize(DurabilityData data) {
            _data = data;
            ResetDurability();
        }

        private void OnEnable() {
            if (_data == null) {
                return;
            }
            ResetDurability();
        }

        private void ResetDurability() {
            Durability = _data.Durability;
        }

        public void DealDamage(float damage) {
            if (Durability < 0f) {
                return;
            }

            Durability -= damage;
            OnDurabilityChanged?.Invoke(Durability);
            if (Durability < 0f) {
                _signalBus.Fire(new SignalUnitDestroyed(gameObject));
            }
        }
    }
}