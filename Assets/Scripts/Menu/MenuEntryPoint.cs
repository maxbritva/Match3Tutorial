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

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel,
            LevelSequenceView sequenceView, MenuView menuView)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _sequenceView = sequenceView;
            _menuView = menuView;
        }

        public async void Initialize()
        {
           await _setupLevel.Setup(3);
           // music menu
           _sequenceView.SetupButtonsView(3);
           _sceneLoading.LoadingIsDone(true);
           await _menuView.StartAnimation();
           // button enabled
        }
    }
}