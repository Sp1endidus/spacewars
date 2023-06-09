using UnityEngine;

namespace SpaceWars.Runtime.Configs.Movement {
    [CreateAssetMenu(fileName = "Movement", menuName = "SpaceWars/Configs/Movement")]
    public class MovementData : ScriptableObject {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}