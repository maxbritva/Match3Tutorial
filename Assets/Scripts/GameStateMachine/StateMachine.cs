using System.Collections.Generic;
using System.Linq;
using Animations;
using Audio;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.UI;
using Game.Utils;
using GameStateMachine.States;
using Levels;

namespace GameStateMachine
{
    public class StateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;
        private GameBoard _gameBoard;
        private Grid _grid;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private TilePool _tilePool;
        private GameProgress _gameProgress;
        private ScoreCalculator _scoreCalculator;
        private AudioManager _audioManager;
        private EndGamePanelView _endGame;
        private LevelConfig _levelConfig;
        private BackgroundTilesSetup _backgroundTilesSetup;
        private BlankTilesSetup _blankTilesSetup;
        private FXPool _fxPool;

        public StateMachine(GameBoard gameBoard, Grid grid, IAnimation animation, MatchFinder matchFinder, LevelConfig levelConfig,
            TilePool tilePool, GameProgress progress, ScoreCalculator scoreCalculator, AudioManager audioManager, FXPool fxPool,
            EndGamePanelView endGame, BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup)
        {
            _fxPool = fxPool;
            _gameBoard = gameBoard;
            _levelConfig = levelConfig;
            _grid = grid;
            _animation = animation;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
            _gameProgress = progress;
            _scoreCalculator = scoreCalculator;
            _backgroundTilesSetup = backgroundTilesSetup;
            _blankTilesSetup = blankTilesSetup;
            _audioManager = audioManager;
            _endGame = endGame;
            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard, _levelConfig, backgroundTilesSetup, blankTilesSetup),
                new PlayerTurnState(_grid, this, _animation, _audioManager),
                new SwapTilesState(_grid, this, _animation, _matchFinder, _gameProgress, _audioManager),
                new RemoveTilesState(grid, this, _animation, _gameBoard, _matchFinder, _scoreCalculator, _audioManager,_fxPool),
                new RefillGridState(grid,this,_animation, _matchFinder, _tilePool, _gameBoard.transform, _gameProgress, _audioManager),
                new WinState(_endGame),
                new LooseState(_endGame),
            };
            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}