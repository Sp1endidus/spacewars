using UnityEngine;
using Zenject;

namespace SpaceWars.Runtime.Bootstrap {
    public class BootstrapController : MonoBehaviour {
        private SceneController _sceneController;

        [Inject]
        private void Construct(SceneController sceneController) {
            _sceneController = sceneController;
        }

        private void Start() {
            LoadAsync();
        }

        private async void LoadAsync() {
            await _sceneController.LoadCoreAsync();
        }
    }
}