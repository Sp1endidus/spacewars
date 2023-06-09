using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Gameplay.Model.Asteroid;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Controllers {
    public class AsteroidController : MonoBehaviour {
        [SerializeField] private Transform[] spawnPoints;

        private AsteroidModel.Pool _asteroidPool;
        private ConfigsController _configsController;

        [Inject]
        private void Construct(AsteroidModel.Pool asteroidPool,
            ConfigsController configsController) {
            _asteroidPool = asteroidPool;
            _configsController = configsController;
        }

        private void Start() {
            SpawnAsteroids();
        }

        private void SpawnAsteroids() {
            var amount = _configsController.AsteroidData.Amount;

            for (int i = 0; i < amount; i++) {
                var pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                var size = Random.Range(_configsController.AsteroidData.SizeMin,
                    _configsController.AsteroidData.SizeMax);
                _asteroidPool.Spawn(pos, size);
            }
        }
    }
}