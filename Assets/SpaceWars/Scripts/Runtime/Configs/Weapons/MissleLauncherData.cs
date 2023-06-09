using UnityEngine;

namespace SpaceWars.Runtime.Configs.Weapons {
    [CreateAssetMenu(fileName = "MissleLauncher", menuName = "SpaceWars/Configs/Weapons/MissleLauncher")]
    public class MissleLauncherData : ScriptableObject {
        [field: SerializeField] public float ReloadTime { get; private set; }
    }
}