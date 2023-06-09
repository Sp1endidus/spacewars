using Cysharp.Threading.Tasks;
using SpaceWars.Runtime.Bootstrap;
using SpaceWars.Runtime.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace SpaceWars.Runtime.Ui.Core.Windows {
    public class WinWindow : WindowBase {
        [SerializeField] private TMP_Text label;
        [SerializeField] private Button nextLevelButton;

        private SceneController _sceneController;
        private CoreController _coreController;

        private const string LevelCompletedText = "Level {0} completed!";

        [Inject]
        private void Construct(SceneController sceneController,
            CoreController coreController) {
            _sceneController = sceneController;
            _coreController = coreController;
        }

        private void Start() {
            nextLevelButton.onClick.AddListener(StartGameplay);
        }

        public override void Show() {
            base.Show();
            label.text = string.Format(LevelCompletedText, _coreController.CurrentLevel);
            _sceneController.UnloadGameplay().Forget();
        }

        private async void StartGameplay() {
            await _coreController.StartGameplay(true);
            Hide();
        }
    }
}