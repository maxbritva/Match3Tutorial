using System;
using System.Collections.Generic;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using Game.Board;
using Game.GridSystem;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using Game.Utils;

namespace GameStateMachine.States
{
    public class RemoveTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private AudioManager _audioManager;
        private ScoreCalculator _scoreCalculator;
        private FXPool _fxPool;
        private GameBoard _gameBoard;

        public RemoveTilesState(Grid grid, IStateSwitcher stateSwitcher, IAnimation animation, GameBoard gameBoard,
            MatchFinder matchFinder, ScoreCalculator scoreCalculator, AudioManager audioManager, FXPool fxPool)
        {
            _gameBoard = gameBoard;
            _fxPool = fxPool;
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _scoreCalculator = scoreCalculator;
            _matchFinder = matchFinder;
            _audioManager = audioManager;
        }

        public void Dispose() => _cts?.Dispose();
        public async void Enter()
        {
            _cts = new CancellationTokenSource();
            _scoreCalculator.CalculateScoreToAdd(_matchFinder.CurrentMatchResult.MatchDirection);
            await RemoveTiles(_matchFinder.TilesToRemove, _grid);
            _stateSwitcher.SwitchState<RefillGridState>();
        }

        public void Exit()
        {
            _matchFinder.ClearTilesToRemove();
            _cts?.Cancel();
        }

        private async UniTask RemoveTiles(List<Tile> tilesToRemove, Grid grid)
        {
            foreach (var tile in tilesToRemove)
            {
                _audioManager.PlayRemove();
                var pos = grid.WorldToGrid(tile.transform.position);
                grid.SetValue(pos.x, pos.y, null);
                await _animation.HideTile(tile.gameObject);
                _fxPool.GetFXFromPool(tile.transform.position, _gameBoard.transform);
            }
            _cts.Cancel();
        }
    }
}