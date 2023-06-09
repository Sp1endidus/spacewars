using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons {
    public interface IWeapon {
        WeaponType WeaponType { get; }
        float ReloadTime { get; }
        void Fire();
        void SetSource(Transform source);
        void ClearSource();
    }

    public class WeaponPool : IInitializable {
        public class Pool : MemoryPool<Transform, IWeapon> {
            protected override void Reinitialize(Transform source, IWeapon iWeapon) {
                iWeapon.SetSource(source);
            }

            protected override void OnDespawned(IWeapon iWeapon) {
                iWeapon.ClearSource();
            }
        }

        public class WeaponFactory<T> : IFactory<T> where T : IWeapon {
            private readonly DiContainer _diContainer;

            public WeaponFactory(DiContainer diContainer) {
                _diContainer = diContainer;
            }

            public T Create() {
                return _diContainer.Instantiate<T>();
            }
        }

        private readonly DiContainer _diContainer;
        private readonly MemoryPoolSettings _settings;

        private readonly Dictionary<WeaponType, MemoryPool<Transform, IWeapon>> _pools;

        [Inject]
        private WeaponPool(DiContainer diContainer,
            MemoryPoolSettings settings) {
            _diContainer = diContainer;
            _settings = settings;
            _pools = new Dictionary<WeaponType, MemoryPool<Transform, IWeapon>>();
        }

        public void Initialize() {
            _pools.Add(WeaponType.Laser, _diContainer.Instantiate<LaserModel.Pool>(new object[] {
                _settings, new WeaponFactory<LaserModel>(_diContainer) }));
            _pools.Add(WeaponType.MissleLauncher, _diContainer.Instantiate<MissleLauncherModel.Pool>(new object[] {
                _settings, new WeaponFactory<MissleLauncherModel>(_diContainer) }));
            _pools.Add(WeaponType.Machinegun, _diContainer.Instantiate<MachinegunModel.Pool>(new object[] {
                _settings, new WeaponFactory<MachinegunModel>(_diContainer) }));
        }

        public IWeapon Spawn(WeaponType weaponType, Transform source) {
            return _pools[weaponType].Spawn(source);
        }

        public void Despawn(IWeapon iWeapon) {
            _pools[iWeapon.WeaponType].Despawn(iWeapon);
        }
    }

    public enum WeaponType {
        Laser,
        MissleLauncher,
        Machinegun
    }
}