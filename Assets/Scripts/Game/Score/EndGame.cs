using Audio;
using Data;
using save;
using SceneLoading;
using UnityEditor.Overlays;

namespace Game.Score
{
    public class EndGame
    {
        private GameData _gameData;
        private SaveProgress _progress;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;

        public EndGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading sceneLoading, SaveProgress saveProgress)
        {
            _progress = saveProgress;
            _gameData = gameData;
            _audioManager = audioManager;
            _sceneLoading = sceneLoading;
        }

        public async void End(bool success)
        {
            if(success && _gameData.CurrentLevel.LevelNumber == _gameData.CurrentLevelIndex)
                _gameData.OpenNextLevel();
            _progress.SaveData();
            _audioManager.StopMusic();
            await _sceneLoading.UnloadAsync(Scenes.GAME);
            await _sceneLoading.LoadAsync(Scenes.MENU);
            _audioManager.PlayMenuMusic();
        }
    }
}