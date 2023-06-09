using UnityEngine;

namespace SpaceWars.Runtime.Configs.Difficult {
    [CreateAssetMenu(fileName = "Difficult", menuName = "SpaceWars/Configs/Difficult")]
    public class DifficultData : ScriptableObject {
        [field: SerializeField] public int EnemiesAmountBase { get; private set; }
        [field: SerializeField] public float EnemiesAmountMultiplier { get; private set; }
        [field: SerializeField] public float DelayBetweenSpawnInSeconds { get; private set; }
        [field: SerializeField] public int EnemiesToDestroyBase { get; private set; }
        [field: SerializeField] public float EnemiesToDestroyMultiplier { get; private set; }
    }
}