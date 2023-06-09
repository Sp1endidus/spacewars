using SpaceWars.Runtime.Core;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons;
using SpaceWars.Runtime.Gameplay.Model.Unit;
using SpaceWars.Runtime.Signals.Gameplay;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Ui.Gameplay {
    public class GameplayUiController : MonoBehaviour {
        [SerializeField] private TMP_Text durability;
        [SerializeField] private TMP_Text progress;
        [SerializeField] private TMP_Text selectedWeapon;

        private CoreController _coreController;
        private SignalBus _signalBus;
        private UnitModel _player;

        private StringBuilder _progressSb = new StringBuilder(5);

        [Inject]
        private void Construct(CoreController coreController,
            SignalBus signalBus) {
            _coreController = coreController;
            _signalBus = signalBus;

            _signalBus.Subscribe<SignalPlayerBinding>(OnPlayerBinding);
            _signalBus.Subscribe<SignalUnitDestroyed>(OnEnemyDestroyed);
        }

        private void OnDisable() {
            _signalBus.Unsubscribe<SignalPlayerBinding>(OnPlayerBinding);
            _signalBus.Unsubscribe<SignalUnitDestroyed>(OnEnemyDestroyed);
        }

        private void OnPlayerBinding(SignalPlayerBinding signal) {
            _player = signal.Player.GetComponent<UnitModel>();
            _player.DurabilityModel.OnDurabilityChanged += OnDurabilityChanged;
            _player.ShootingModel.OnWeaponSelected += OnWeaponSelected;
            OnEnemyDestroyed();
            OnDurabilityChanged(_player.DurabilityModel.Durability);
            OnWeaponSelected(WeaponType.Laser);
        }

        private void OnEnemyDestroyed() {
            _progressSb.Clear();
            _progressSb.Append(_coreController.EnemiesDestroyed);
            _progressSb.Append("/");
            _progressSb.Append(_coreController.EnemiesToDestroy);
            progress.text = _progressSb.ToString();
        }

        private void OnDurabilityChanged(float newDurability) {
            durability.text = newDurability.ToString();
        }

        private void OnWeaponSelected(WeaponType weaponType) {
            selectedWeapon.text = weaponType.ToString();
        }
    }
}