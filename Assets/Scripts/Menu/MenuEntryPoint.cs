using Audio;
using Data;
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
        private GameData _gameData;

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel,
            LevelSequenceView sequenceView, MenuView menuView, AudioManager audioManager, GameData gameData)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _sequenceView = sequenceView;
            _menuView = menuView;
            _audioManager = audioManager;
            _gameData = gameData;
        }

        public async void Initialize()
        {
           await _setupLevel.Setup(_gameData.CurrentLevelIndex);
           _sequenceView.SetupButtonsView(_gameData.CurrentLevelIndex);
           _audioManager.PlayMenuMusic();
           _sceneLoading.LoadingIsDone(true);
           await _menuView.StartAnimation();
        }
    }
}