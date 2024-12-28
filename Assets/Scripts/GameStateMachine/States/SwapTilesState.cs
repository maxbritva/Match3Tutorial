using System;
using System.Threading;
using Animations;
using Audio;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.MatchTiles;
using Game.Score;
using Game.Tiles;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace GameStateMachine.States
{
    public class SwapTilesState : IState, IDisposable
    {
        private CancellationTokenSource _cts;
        private Grid _grid;
        private IStateSwitcher _stateSwitcher;
        private IAnimation _animation;
        private MatchFinder _matchFinder;
        private GameProgress _progress;
        private AudioManager _audioManager;

        public SwapTilesState(Grid grid, IStateSwitcher stateSwitcher, IAnimation animation, MatchFinder matchFinder, GameProgress progress, AudioManager audioManager)
        {
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _matchFinder = matchFinder;
            _audioManager = audioManager;
            _progress = progress;
        }
        public void Dispose() => _cts?.Dispose();

        public async void Enter()
        {
           _cts = new CancellationTokenSource();
           _audioManager.PlayWhoosh();
           await SwapTiles(_grid.CurrentPosition, _grid.TargetPosition);
           if (_matchFinder.CheckBoardForMatches(_grid) == false)
           {
               _audioManager.PlayNoMatch();
               await SwapTiles(_grid.TargetPosition, _grid.CurrentPosition);
               _stateSwitcher.SwitchState<PlayerTurnState>();
           }
           else
           {
               _audioManager.PlayMatch();
               _progress.SpendMoves();
               _stateSwitcher.SwitchState<RemoveTilesState>();
           }
        }

        public void Exit() => _cts?.Cancel();

        private async UniTask SwapTiles(Vector2Int current, Vector2Int target)
        {
            var currentTile = _grid.GetValue(current.x, current.y);
            var targetTile = _grid.GetValue(target.x, target.y);

            MoveAnimation(currentTile, target);
            MoveAnimation(targetTile, current);
            
            _grid.SetValue(current.x,current.y, targetTile);
            _grid.SetValue(target.x,target.y, currentTile);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), _cts.IsCancellationRequested);
        }

        private void MoveAnimation(Tile tileToMove, Vector2Int position) =>
            _animation.MoveTile(tileToMove, _grid.GridToWorld(position.x, position.y), Ease.OutCubic);
    }
}