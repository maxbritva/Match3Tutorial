using System;
using Animations;
using Audio;
using Game.Tiles;
using Input;
using UnityEngine;
using Grid = Game.GridSystem.Grid;

namespace GameStateMachine.States
{
    public class PlayerTurnState: IState, IDisposable
    {
        private readonly Vector2Int _emptyPosition = Vector2Int.one * -1;
        private readonly InputReader _inputReader;
        private readonly Grid _grid;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Camera _camera;
        private IAnimation _animation;
        private AudioManager _audioManager;


        public PlayerTurnState(Grid grid, IStateSwitcher stateSwitcher, IAnimation animation, AudioManager audioManager)
        {
            _inputReader = new InputReader();
            _grid = grid;
            _stateSwitcher = stateSwitcher;
            _animation = animation;
            _camera = Camera.main;
            _audioManager = audioManager;
            _inputReader.Click += OnTileClick;
        }
        public void Dispose() => _inputReader.Click -= OnTileClick;

        public void Enter()
        {
            _inputReader.EnableInputs(true);
            DeselectTile();
        }

        public void Exit() => _inputReader.EnableInputs(false);


        private void OnTileClick()
        {
            var clickPosition = _grid.WorldToGrid(
                _camera.ScreenToWorldPoint(_inputReader.Position()));
            if(IsValidPosition(clickPosition) == false || IsBlankPosition(clickPosition))
                return;
            if (_grid.CurrentPosition == _emptyPosition)
            {
                _audioManager.PlayClick();
                _grid.SetCurrentPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1.2f);
            }
            
            else if (_grid.CurrentPosition == clickPosition)
            {
                _audioManager.PlayDeselect();
                DeselectTile();
            }
            
            else if (_grid.CurrentPosition != clickPosition && 
                     IsSwappable(_grid.CurrentPosition, clickPosition))
            {
                _grid.SetTargetPosition(clickPosition);
                _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, 
                    _grid.CurrentPosition.y), 1f);
                _stateSwitcher.SwitchState<SwapTilesState>();
            }
        }

        private void DeselectTile()
        {
            _animation.AnimateTile(_grid.GetValue(_grid.CurrentPosition.x, _grid.CurrentPosition.y), 1f);
            _grid.SetCurrentPosition(_emptyPosition);
            _grid.SetTargetPosition(_emptyPosition);
        }

        private bool IsSwappable(Vector2Int currentTilePos, Vector2Int targetTilePos) => 
            Mathf.Abs(currentTilePos.x - targetTilePos.x) + 
            Mathf.Abs(currentTilePos.y - targetTilePos.y) == 1;

        private bool IsBlankPosition(Vector2Int gridPos) => 
            _grid.GetValue(gridPos.x, gridPos.y).TileConfig.TileKind == TileKind.Blank;

        private bool IsValidPosition(Vector2Int gridPos) => 
            gridPos.x >= 0 && gridPos.x < _grid.Width && 
            gridPos.y >= 0 && gridPos.y < _grid.Height;
    }
}