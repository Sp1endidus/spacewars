using SpaceWars.Runtime.Gameplay.Model.Asteroid;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons;
using SpaceWars.Runtime.Gameplay.Model.Unit;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay {
    public class GameplaySceneInstaller : MonoInstaller {
        [Header("Enemies")]
        [SerializeField] private UnitModel enemyPrefab;
        [SerializeField] private Transform enemiesRoot;
        [SerializeField] private int enemiesPrespawnAmount;

        [Space, Header("Asteroids")]
        [SerializeField] private AsteroidModel asteroidPrefab;
        [SerializeField] private Transform asteroidsRoot;
        [SerializeField] private int asteroidsPrespawnAmount;

        [Space, Header("Player")]
        [SerializeField] private GameObject player;

        [Space, Header("Shooting")]
        [SerializeField] private MemoryPoolSettings weaponPoolSettings;
        [SerializeField] private MemoryPoolSettings ammoPoolSettings;

        public override void InstallBindings() {
            Container.BindMemoryPool<UnitModel, UnitModel.EnemyPool>()
                .WithInitialSize(enemiesPrespawnAmount)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransform(enemiesRoot);

            Container.BindMemoryPool<AsteroidModel, AsteroidModel.Pool>()
                .WithInitialSize(asteroidsPrespawnAmount)
                .FromComponentInNewPrefab(asteroidPrefab)
                .UnderTransform(asteroidsRoot);

            Container.Bind<UnitModel>().FromComponentOn(player).AsSingle();

            Container.BindInterfacesAndSelfTo<WeaponPool>()
                .AsSingle().WithArguments(weaponPoolSettings);
            Container.BindInterfacesAndSelfTo<AmmoPool>()
                .AsSingle().WithArguments(ammoPoolSettings);
        }
    }
}