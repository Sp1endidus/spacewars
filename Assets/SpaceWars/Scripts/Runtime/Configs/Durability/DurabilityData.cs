using UnityEngine;

namespace SpaceWars.Runtime.Configs.Durability {
    [CreateAssetMenu(fileName = "Durability", menuName = "SpaceWars/Configs/Durability")]
    public class DurabilityData : ScriptableObject {
        [field: SerializeField] public float Durability { get; private set; }
    }
}