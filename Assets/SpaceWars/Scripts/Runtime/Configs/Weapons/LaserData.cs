using UnityEngine;

namespace SpaceWars.Runtime.Configs.Weapons {
    [CreateAssetMenu(fileName = "Laser", menuName = "SpaceWars/Configs/Weapons/Laser")]
    public class LaserData : ScriptableObject {
        [field: SerializeField] public float Distance { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}