using UnityEngine;

namespace SpaceWars.Runtime.Configs.Weapons {
    [CreateAssetMenu(fileName = "Machinegun", menuName = "SpaceWars/Configs/Weapons/Machinegun")]
    public class MachinegunData : ScriptableObject {
        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}