using System.Threading;
using Audio;
using Data;
using Levels;
using SceneLoading;
using UnityEngine;

namespace Menu
{
    public class StartGame
    {
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;
        private CancellationTokenSource _cts;

        public StartGame(GameData gameData, AudioManager audioManager, IAsyncSceneLoading sceneLoading)
        {
            _gameData = gameData;
            _audioManager = audioManager;
            _sceneLoading = sceneLoading;
        }

        public async void Start(LevelConfig level)
        {
            _cts = new CancellationTokenSource();
            _gameData.SetCurrentLevel(level);
            _audioManager.StopMusic();
            _audioManager.PlayStopMusic();
            await _sceneLoading.UnloadAsync(Scenes.MENU);
            await _sceneLoading.LoadAsync(Scenes.GAME);
            _audioManager.PlayGameMusic();
            _cts.Cancel();
        }
    }
}