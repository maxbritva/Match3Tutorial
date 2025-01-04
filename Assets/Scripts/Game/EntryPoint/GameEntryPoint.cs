using Animations;
using Audio;
using Data;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using GameStateMachine;
using Levels;
using ResourcesLoading;
using SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Game.GridSystem.Grid;

namespace Game.EntryPoint
{
    public class GameEntryPoint : IInitializable
    {

        private BackgroundTilesSetup _backgroundTilesSetup;
        private LevelConfig _levelConfig;
        private ScoreCalculator _scoreCalculator;
        private BlankTilesSetup _blankTilesSetup;
        private StateMachine _stateMachine;
        private GameProgress _progress;
        private MatchFinder _matchFinder;
        private Grid _grid;
        private GameBoard _gameBoard;
        private GameDebug _gameDebug;
        private TilePool _tilePool;
        private GameData _gameData;
        private AudioManager _audioManager;
        private IAnimation _animation;
        private GameResourcesLoader _resourcesLoader;
        private SetupCamera _setupCamera;
        // FX pool
        private IAsyncSceneLoading _sceneLoading;
        private EndGamePanelView _endGame;

        private bool _isDebugging;

        public GameEntryPoint(ScoreCalculator scoreCalculator, BlankTilesSetup blankTilesSetup, GameProgress progress, MatchFinder matchFinder, Grid grid, GameBoard gameBoard, 
            GameDebug gameDebug, TilePool tilePool, GameData gameData, AudioManager audioManager, IAnimation animation, BackgroundTilesSetup backgroundTilesSetup,
            GameResourcesLoader resourcesLoader, SetupCamera setupCamera, IAsyncSceneLoading sceneLoading, EndGamePanelView endGame)
        {
            _backgroundTilesSetup = backgroundTilesSetup;
            _scoreCalculator = scoreCalculator;
            _blankTilesSetup = blankTilesSetup;
            _progress = progress;
            _matchFinder = matchFinder;
            _grid = grid;
            _gameBoard = gameBoard;
            _gameDebug = gameDebug;
            _tilePool = tilePool;
            _gameData = gameData;
            _audioManager = audioManager;
            _animation = animation;
            _resourcesLoader = resourcesLoader;
            _setupCamera = setupCamera;
            _sceneLoading = sceneLoading;
            _endGame = endGame;
        }

        public void Initialize()
        {
            _levelConfig = _gameData.CurrentLevel;
            if(_isDebugging)
                _gameDebug.ShowDebug(_gameBoard.transform);
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _progress.LoadLevelConfig(_levelConfig.GoalScore, _levelConfig.Moves);
            // await resources
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);
            _blankTilesSetup.SetupBlanks(_levelConfig);
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _levelConfig , _tilePool,
                _progress, _scoreCalculator, _audioManager, _endGame, _backgroundTilesSetup, _blankTilesSetup);
            _sceneLoading.LoadingIsDone(true);
        }
    }
}