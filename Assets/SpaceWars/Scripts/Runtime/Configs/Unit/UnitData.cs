using SpaceWars.Runtime.Configs.Durability;
using SpaceWars.Runtime.Configs.Movement;
using UnityEngine;

namespace SpaceWars.Runtime.Configs.Unit {
    [CreateAssetMenu(fileName = "Unit", menuName = "SpaceWars/Configs/Unit")]
    public class UnitData : ScriptableObject {
        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public DurabilityData DurabilityData { get; private set; }
        [field: SerializeField] public MovementData MovementData { get; private set; }

    }
}