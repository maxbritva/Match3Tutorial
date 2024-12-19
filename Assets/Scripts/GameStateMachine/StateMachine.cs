using System.Collections.Generic;
using System.Linq;
using Animations;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using GameStateMachine.States;

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

        public StateMachine(GameBoard gameBoard, Grid grid, IAnimation animation, MatchFinder matchFinder, 
            TilePool tilePool, GameProgress progress, ScoreCalculator scoreCalculator)
        {
            _gameBoard = gameBoard;
            _grid = grid;
            _animation = animation;
            _tilePool = tilePool;
            _matchFinder = matchFinder;
            _gameProgress = progress;
            _scoreCalculator = scoreCalculator;
            _states = new List<IState>()
            {
                new PrepareState(this, _gameBoard),
                new PlayerTurnState(_grid, this, _animation),
                new SwapTilesState(_grid, this, _animation, _matchFinder, _gameProgress),
                new RemoveTilesState(grid, this, _animation, _matchFinder, _scoreCalculator),
                new RefillGridState(grid,this,_animation, _matchFinder, _tilePool, _gameBoard.transform, _gameProgress),
                new WinState(),
                new LooseState(),
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