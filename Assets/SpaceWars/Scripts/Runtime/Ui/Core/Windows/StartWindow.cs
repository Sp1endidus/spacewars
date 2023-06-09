using SpaceWars.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceWars.Runtime.Ui.Core.Windows {
    public class StartWindow : WindowBase {
        [SerializeField] private Button startButton;

        private CoreController _coreController;

        [Inject]
        private void Construct(CoreController coreController) {
            _coreController = coreController;
        }

        private void Start() {
            startButton.onClick.AddListener(StartGameplay);
        }

        private async void StartGameplay() {
            await _coreController.StartGameplay(false);
            Hide();
        }
    }
}