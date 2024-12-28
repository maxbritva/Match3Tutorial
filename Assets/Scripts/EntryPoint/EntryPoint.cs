using System;
using Animations;
using Audio;
using Game.Board;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using GameStateMachine;
using Levels;
using SceneLoading;
using UnityEngine;
using VContainer;
using Grid = Game.GridSystem.Grid;

namespace EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        private StateMachine _stateMachine;
        private Grid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _progress;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private IAsyncSceneLoading _sceneLoading;
        
        private void Start()
        {
            _stateMachine = new StateMachine(_gameBoard, _grid, _animation, _matchFinder, _tilePool, _progress, _scoreCalculator, _audioManager);
            _progress.LoadLevelConfig(_gameBoard.LevelConfig.GoalScore, _gameBoard.LevelConfig.Moves);
            _sceneLoading.LoadingIsDone(true);
        }


        [Inject] private void Construct(Grid grid, IAnimation animation, MatchFinder matchFinder, TilePool tilePool, 
            GameProgress progress, ScoreCalculator scoreCalculator, AudioManager audioManager, IAsyncSceneLoading asyncSceneLoading)
        {
            _grid = grid;
            _animation = animation;
            _matchFinder = matchFinder;
            _tilePool = tilePool;
            _progress = progress;
            _audioManager = audioManager;
            _scoreCalculator = scoreCalculator;
            _sceneLoading = asyncSceneLoading;
        }
    }
}