using SpaceWars.Runtime.Configs;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo {
    public class AmmoPool : IInitializable {
        public class Pool : MonoMemoryPool<AmmoBase> {
            protected override void Reinitialize(AmmoBase ammoBase) {
            }

            protected override void OnDespawned(AmmoBase ammoBase) {
                ammoBase.gameObject.SetActive(false);
            }
        }

        public class AmmoFactory<T> : IFactory<T> where T : AmmoBase {
            private readonly DiContainer _diContainer;
            private readonly Object _prefab;

            public AmmoFactory(DiContainer diContainer,
                Object prefab) {
                _diContainer = diContainer;
                _prefab = prefab;
            }

            public T Create() {
                return _diContainer.InstantiatePrefabForComponent<T>(_prefab);
            }
        }

        private readonly DiContainer _diContainer;
        private readonly MemoryPoolSettings _settings;
        private readonly ConfigsController _configsController;

        private readonly Dictionary<AmmoType, MonoMemoryPool<AmmoBase>> _pools;

        [Inject]
        private AmmoPool(DiContainer diContainer,
            MemoryPoolSettings settings,
            ConfigsController configsController) {
            _diContainer = diContainer;
            _settings = settings;
            _configsController = configsController;
            _pools = new Dictionary<AmmoType, MonoMemoryPool<AmmoBase>>();
        }

        public void Initialize() {
            _pools.Add(AmmoType.Missle, _diContainer.Instantiate<MissleModel.Pool>(new object[] {
                _settings, new AmmoFactory<MissleModel>(_diContainer, _configsController.MissleData.Prefab) }));
            _pools.Add(AmmoType.Bullet, _diContainer.Instantiate<BulletModel.Pool>(new object[] {
                _settings, new AmmoFactory<BulletModel>(_diContainer, _configsController.BulletData.Prefab) }));
        }

        public AmmoBase Spawn(AmmoType ammoType) {
            return _pools[ammoType].Spawn();
        }

        public void Despawn(AmmoBase ammoBase) {
            _pools[ammoBase.AmmoType].Despawn(ammoBase);
        }
    }

    public enum AmmoType {
        Missle,
        Bullet
    }
}