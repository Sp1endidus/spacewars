using SpaceWars.Runtime.Configs.Ammo;
using SpaceWars.Runtime.Configs.Asteroid;
using SpaceWars.Runtime.Configs.Difficult;
using SpaceWars.Runtime.Configs.Unit;
using SpaceWars.Runtime.Configs.Weapons;
using UnityEngine;

namespace SpaceWars.Runtime.Configs {
    public class ConfigsController : MonoBehaviour {
        [field: SerializeField] public DifficultData DifficultData { get; private set; }

        [field: SerializeField] public LaserData LaserData { get; private set; }
        [field: SerializeField] public MissleLauncherData MissleLauncherData { get; private set; }
        [field: SerializeField] public MachinegunData MachinegunData { get; private set; }

        [field: SerializeField] public MissleData MissleData { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }


        [field: SerializeField] public UnitData PlayerData { get; private set; }
        [field: SerializeField] public UnitData EnemyData { get; private set; }

        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
    }
}