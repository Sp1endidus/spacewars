using SpaceWars.Runtime.Configs.Movement;
using UnityEngine;

namespace SpaceWars.Runtime.Configs.Ammo {
    [CreateAssetMenu(fileName = "Bullet", menuName = "SpaceWars/Configs/Ammo/Bullet")]
    public class BulletData : ScriptableObject {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTimeSeconds { get; private set; }
        [field: SerializeField] public Vector2 Indent { get; private set; }
        [field: SerializeField] public MovementData MovementData { get; private set; }
    }
}