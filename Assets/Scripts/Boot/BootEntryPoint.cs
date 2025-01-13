using DG.Tweening;
using save;
using SceneLoading;
using UnityEngine;
using VContainer.Unity;

namespace Boot
{
    public class BootEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SaveProgress _saveProgress;

        public BootEntryPoint(IAsyncSceneLoading sceneLoading, SaveProgress saveProgress)
        {
            _saveProgress = saveProgress;
            _sceneLoading = sceneLoading;
        }

        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.SetTweensCapacity(5000, 100);
            _saveProgress.LoadData();
            await _sceneLoading.LoadAsync(Scenes.MENU);
        }
    }
}