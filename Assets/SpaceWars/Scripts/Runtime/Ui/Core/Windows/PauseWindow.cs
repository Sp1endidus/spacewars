using SpaceWars.Runtime.Bootstrap;
using SpaceWars.Runtime.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceWars.Runtime.Ui.Core.Windows {
    public class PauseWindow : WindowBase {
        [SerializeField] private Button restartButton;

        private SceneController _sceneController;
        private CoreController _coreController;

        [Inject]
        private void Construct(SceneController sceneController,
            CoreController coreController) {
            _sceneController = sceneController;
            _coreController = coreController;
        }

        private void Start() {
            restartButton.onClick.AddListener(StartGameplay);
        }

        private async void StartGameplay() {
            await _sceneController.UnloadGameplay();
            await _coreController.StartGameplay(false);
            Hide();
            _coreController.OnPause();
        }
    }
}
