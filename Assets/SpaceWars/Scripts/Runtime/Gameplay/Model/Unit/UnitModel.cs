using SpaceWars.Runtime.Configs.Unit;
using SpaceWars.Runtime.Gameplay.Model.Durability;
using SpaceWars.Runtime.Gameplay.Model.Movement;
using SpaceWars.Runtime.Gameplay.Model.Shooting;
using SpaceWars.Runtime.Signals.Gameplay;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Unit {
    public class UnitModel : MonoBehaviour {
        public class EnemyPool : MonoMemoryPool<Vector2, UnitData, UnitModel> {
            protected override void Reinitialize(Vector2 position, UnitData unitData, UnitModel unitModel) {
                unitModel.transform.position = position;
                unitModel.Initialize(unitData);
            }

            protected override void OnDespawned(UnitModel unitModel) {
                unitModel.gameObject.SetActive(false);
            }
        }

        [SerializeField] private MovementModel movementModel;
        [SerializeField] private DurabilityModel durabilityModel;
        [SerializeField] private ShootingModel shootingModel;

        public UnitData Data { get; private set; }
        public DurabilityModel DurabilityModel => durabilityModel;
        public ShootingModel ShootingModel => shootingModel;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
        }

        public void Initialize(UnitData data) {
            Data = data;
            movementModel.Initialize(Data.MovementData);
            durabilityModel.Initialize(Data.DurabilityData);
            gameObject.SetActive(true);

            if (Data.IsPlayer) {
                _signalBus.Fire(new SignalPlayerBinding(gameObject));
            }
        }
    }
}