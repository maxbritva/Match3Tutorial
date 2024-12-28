using Audio;
using Menu.Levels;
using Menu.UI;
using SceneLoading;
using VContainer.Unity;

namespace Menu
{
    public class MenuEntryPoint : IInitializable
    {
        private IAsyncSceneLoading _sceneLoading;
        private SetupLevelSequence _setupLevel;
        private LevelSequenceView _sequenceView;
        private MenuView _menuView;
        private AudioManager _audioManager;

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel,
            LevelSequenceView sequenceView, MenuView menuView, AudioManager audioManager)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _sequenceView = sequenceView;
            _menuView = menuView;
            _audioManager = audioManager;
        }

        public async void Initialize()
        {
           await _setupLevel.Setup(3);
           _audioManager.PlayMenuMusic();
           _sceneLoading.LoadingIsDone(true);
           await _menuView.StartAnimation();
           _sequenceView.SetupButtonsView(3);
        }
    }
}