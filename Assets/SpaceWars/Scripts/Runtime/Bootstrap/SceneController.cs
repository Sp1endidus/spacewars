using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SpaceWars.Runtime.Bootstrap {
    public class SceneController : MonoBehaviour {
        [Header("Core")]
        [SerializeField] private AssetReference core;
        [SerializeField] private AssetReference coreUi;

        [Space, Header("Gameplay")]
        [SerializeField] private AssetReference gameplay;
        [SerializeField] private AssetReference gameplayUi;

        public async UniTask LoadCoreAsync() {
            await LoadSceneAsync(core);
            await LoadSceneAsync(coreUi);
        }

        public async UniTask LoadGameplayAsync() {
            await LoadSceneAsync(gameplayUi);
            await LoadSceneAsync(gameplay);
        }

        public async UniTask UnloadGameplay() {
            await UnloadSceneAsync(gameplay);
            await UnloadSceneAsync(gameplayUi);
        }

        private async UniTask LoadSceneAsync(AssetReference asset,
            LoadSceneMode mode = LoadSceneMode.Additive) {
            var handler = asset.LoadSceneAsync(mode);
            await handler.Task;
        }

        private async UniTask UnloadSceneAsync(AssetReference asset) {
            var handler = asset.UnLoadScene();
            await handler.Task;
        }
    }
}