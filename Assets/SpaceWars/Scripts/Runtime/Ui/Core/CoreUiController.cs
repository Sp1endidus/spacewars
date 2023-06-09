using SpaceWars.Runtime.Signals.Gameplay;
using SpaceWars.Runtime.Signals.Ui;
using SpaceWars.Runtime.Ui.Core.Windows;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Ui.Core {
    public class CoreUiController : MonoBehaviour {
        [SerializeField] private WindowBase[] windowPrefabs;

        private SignalBus _signalBus;
        private WindowBase.Factory _windowFactory;

        private Dictionary<Type, WindowBase> _cachedWindows
            = new Dictionary<Type, WindowBase>();

        private WindowBase _currentWindow;

        [Inject]
        private void Construct(SignalBus signalBus,
            WindowBase.Factory windowFactory) {
            _signalBus = signalBus;
            _windowFactory = windowFactory;
        }

        private void OnEnable() {
            _signalBus.Subscribe<SignalLevelCompleted>(OnLevelCompleted);
            _signalBus.Subscribe<SignalPause>(OnPause);
            _signalBus.Subscribe<SignalGameover>(OnGameover);
        }

        private void Start () {
            ShowWindow<StartWindow>();
        }

        private void OnLevelCompleted(SignalLevelCompleted signal) {
            ShowWindow<WinWindow>();
        }

        private void OnPause() {
            if (_currentWindow == null) {
                ShowWindow<PauseWindow>();
                return;
            }

            if (_currentWindow.GetType() == typeof(PauseWindow)) {
                _currentWindow.Hide();
            }
        }

        private void OnGameover() {
            ShowWindow<LoseWindow>();
        }

        public void ShowWindow<T>() where T : WindowBase {
            WindowBase window;
            if (_cachedWindows.ContainsKey(typeof(T))) {
                window = _cachedWindows[typeof(T)];
                window.Show();
                _currentWindow = window;
                return;
            }

            WindowBase prefab = null;
            for (int i = 0; i < windowPrefabs.Length; i++) {
                if (typeof(T) == windowPrefabs[i].GetType()) {
                    prefab = windowPrefabs[i];
                }
            }

            if (prefab == null) {
                Debug.LogError($"Window with type {typeof(T)} not found!");
                return;
            }

            window = _windowFactory.Create(prefab);
            window.transform.SetParent(transform, false);
            _cachedWindows.Add(typeof(T), window);

            window.Show();
            window.OnWindowHided += OnWindowHided;
            _currentWindow = window;
        }

        private void OnWindowHided(WindowBase window) {
            if (_currentWindow == window) {
                _currentWindow = null;
            }
        }
    }
}