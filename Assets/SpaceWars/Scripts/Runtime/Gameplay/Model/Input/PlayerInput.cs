using SpaceWars.Runtime.Gameplay.Model.Movement;
using SpaceWars.Runtime.Gameplay.Model.Shooting;
using SpaceWars.Runtime.Gameplay.Model.Shooting.Weapons;
using SpaceWars.Runtime.Input;
using SpaceWars.Runtime.Signals.Ui;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Gameplay.Model.Input {
    public class PlayerInput : MonoBehaviour {
        [SerializeField] private MovementModel movementModel;
        [SerializeField] private ShootingModel shootingModel;

        private MainActions _mainActions;

        private SignalBus _signalBus;

        private bool _paused;
        private bool _action1Holding;

        [Inject]
        private void Construct(SignalBus signalBus) {
            _signalBus = signalBus;
        }

        private void Start() {
            _mainActions = new MainActions();
            _mainActions.Gameplay.Action1.performed += context => Action1();
            _mainActions.Gameplay.Action1.started += context => _action1Holding = true;
            _mainActions.Gameplay.Action1.canceled += context => _action1Holding = false;
            _mainActions.Gameplay.Weapon1.performed += context => WeaponSelection(WeaponType.Laser);
            _mainActions.Gameplay.Weapon2.performed += context => WeaponSelection(WeaponType.MissleLauncher);
            _mainActions.Gameplay.Weapon3.performed += context => WeaponSelection(WeaponType.Machinegun);
            _mainActions.Gameplay.Pause.performed += context => Pause();
            _mainActions.Enable();
        }

        private void Update() {
            if (_paused) {
                movementModel.SetInput(0f, 0f);
                return;
            }

            var inputValue = _mainActions.Gameplay.Movement.ReadValue<Vector2>();
            movementModel.SetInput(inputValue.y, inputValue.x * -1f);

            if (_action1Holding) {
                Action1();
            }
        }

        private void OnDisable() {
            _mainActions.Disable();
        }

        private void Action1() {
            if (_paused) {
                return;
            }
            shootingModel.Fire();
        }

        private void WeaponSelection(WeaponType weaponType) {
            if (_paused) {
                return;
            }
            shootingModel.SwitchWeapon(weaponType);
        }

        private void Pause() {
            _paused = !_paused;
            _signalBus.Fire(new SignalPause());
        }
    }
}