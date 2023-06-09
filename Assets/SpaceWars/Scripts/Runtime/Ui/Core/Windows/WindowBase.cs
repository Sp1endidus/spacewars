using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceWars.Runtime.Ui.Core.Windows {
    public class WindowBase : MonoBehaviour {
        public class Factory : PlaceholderFactory<UnityEngine.Object, WindowBase> {

        }

        [SerializeField] private Button[] quitButtons;

        public event Action<WindowBase> OnWindowHided;

        private void Start() {
            foreach (var quitButton in quitButtons) {
                quitButton.onClick.AddListener(() => Application.Quit());
            }
        }

        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            gameObject.SetActive(false);
            OnWindowHided?.Invoke(this);
        }
    }
}