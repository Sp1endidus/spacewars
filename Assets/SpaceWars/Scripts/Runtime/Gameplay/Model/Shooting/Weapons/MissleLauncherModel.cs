using SpaceWars.Runtime.Configs;
using SpaceWars.Runtime.Configs.Weapons;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo;
using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons {
    public class MissleLauncherModel : IWeapon {
        public class Pool : WeaponPool.Pool {

        }

        private Transform _source;
        private MissleLauncherData _data;
        private AmmoPool _ammoPool;

        public WeaponType WeaponType => WeaponType.MissleLauncher;
        public float ReloadTime => _data.ReloadTime;

        private MissleLauncherModel(ConfigsController configsController,
            AmmoPool ammoPool) {
            _data = configsController.MissleLauncherData;
            _ammoPool = ammoPool;
        }

        public void SetSource(Transform source) {
            _source = source;
        }

        public void ClearSource() {
            _source = null;
        }

        public void Fire() {
            var ammo = _ammoPool.Spawn(AmmoType.Missle) as MissleModel;
            ammo.transform.position = _source.position + _source.TransformDirection(ammo.Data.Indent);
            ammo.transform.rotation = _source.rotation;
            ammo.gameObject.SetActive(true);
        }
    }
}