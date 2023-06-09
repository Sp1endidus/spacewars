using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Configs.Weapons;
using SpaceWars.Runtime.Gameplay.Model.Durability;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons {
    public class LaserModel : IWeapon {
        public class Pool : WeaponPool.Pool {

        }

        private Transform _source;
        private LaserData _data;

        public WeaponType WeaponType => WeaponType.Laser;
        public float ReloadTime => _data.ReloadTime;

        private LaserModel(ConfigsController configsController) {
            _data = configsController.LaserData;
        }

        public void SetSource(Transform source) {
            _source = source;
        }

        public void ClearSource() {
            _source = null;
        }

        public void Fire() {
            if (_source == null) {
                return;
            }

            var raycastHit = Physics2D.Raycast(_source.position, _source.TransformDirection(Vector2.up), _data.Distance);
            if (!raycastHit.collider) {
                return;
            }

            var durability = raycastHit.collider.GetComponent<DurabilityModel>();
            if (durability == null) {
                return;
            }

            durability.DealDamage(_data.Damage);
        }
    }
}