using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Asteroid {
    public class AsteroidModel : MonoBehaviour {
        public class Pool : MonoMemoryPool<Vector2, float, AsteroidModel> {
            protected override void Reinitialize(Vector2 position, float size, AsteroidModel asteroidModel) {
                asteroidModel.transform.position = position;
                asteroidModel.Initialize(size);
            }

            protected override void OnDespawned(AsteroidModel asteroidModel) {
                asteroidModel.gameObject.SetActive(false);
            }
        }

        public void Initialize(float size) {
            transform.localScale = new Vector2(size, size);
        }
    }
}