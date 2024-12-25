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

        public MenuEntryPoint(IAsyncSceneLoading sceneLoading, SetupLevelSequence setupLevel,
            LevelSequenceView sequenceView)
        {
            _sceneLoading = sceneLoading;
            _setupLevel = setupLevel;
            _sequenceView = sequenceView;
        }

        public async void Initialize()
        {
           await _setupLevel.Setup(3);
           // music menu
           _sequenceView.SetupButtonsView(3);
           _sceneLoading.LoadingIsDone(true);
           // await animation
           // button enabled
        }
    }
}