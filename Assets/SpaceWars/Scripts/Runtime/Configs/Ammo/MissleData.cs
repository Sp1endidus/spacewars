using SpaceWars.Runtime.Configs.Movement;
using UnityEngine;

namespace SpaceWars.Runtime.Configs.Ammo {
    [CreateAssetMenu(fileName = "Missle", menuName = "SpaceWars/Configs/Ammo/Missle")]
    public class MissleData : ScriptableObject {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public int MaxTargets { get; private set; }
        [field: SerializeField] public int BufferSize { get; private set; }
        [field: SerializeField] public float LifeTimeSeconds { get; private set; }
        [field: SerializeField] public Vector2 Indent { get; private set; }
        [field: SerializeField] public MovementData MovementData { get; private set; }
    }
}