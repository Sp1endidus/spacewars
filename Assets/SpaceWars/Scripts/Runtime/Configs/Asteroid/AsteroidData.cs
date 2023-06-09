using UnityEngine;

namespace SpaceWars.Runtime.Configs.Asteroid {
    [CreateAssetMenu(fileName = "Asteroid", menuName = "SpaceWars/Configs/Asteroid")]
    public class AsteroidData : ScriptableObject {
        [field: SerializeField] public int Amount { get; private set; }
        [field: SerializeField] public float SizeMin { get; private set; }
        [field: SerializeField] public float SizeMax { get; private set; }
    }
}