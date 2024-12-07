using System;
using System.Collections.Generic;
using Game.GridSystem;
using Game.Tiles;
using Game.Utils;
using Input;
using Levels;
using UnityEngine;
using VContainer;
using Grid = Game.GridSystem.Grid;

namespace Game.Board
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private bool _isDebugging;
        [SerializeField] private TileConfig _tileConfig;
        private readonly List<Tile> _tilesToRefill = new List<Tile>();

        private Grid _grid;
        private BlankTilesSetup _blankTilesSetup;
        private TilePool _tilePool;
        private SetupCamera _setupCamera;
        private GameDebug _gameDebug;
        private InputReader _inputReader;

        private void Awake()
        {
            _inputReader = new InputReader();
            _inputReader.EnableInputs(true);
            _grid.SetupGrid(_levelConfig.Width, _levelConfig.Height);
            _blankTilesSetup.SetupBlanks(_levelConfig);
            _setupCamera.SetCamera(_grid.Width, _grid.Height, false);
            if(_isDebugging)
                _gameDebug.ShowDebug(transform);
        }
        
        public void CreateBoard()
        {
            FillBoard();
        }

        private void FillBoard()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_blankTilesSetup.Blanks[x, y])
                    {
                        if(_grid.GetValue(x,y)) continue;
                        var blankTile = _tilePool.CreateBlankTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x,y, blankTile);
                    }
                    else
                    {
                        var tile = _tilePool.GetTile(_grid.GridToWorld(x, y), transform);
                        _grid.SetValue(x,y, tile);
                        tile.gameObject.SetActive(true);
                        _tilesToRefill.Add(tile);
                    }
                }
            }
        }


        [Inject] private void Construct (Grid grid, SetupCamera setupCamera, TilePool pool, GameDebug gameDebug, BlankTilesSetup blankTilesSetup)
        {
            _grid = grid;
            _setupCamera = setupCamera;
            _tilePool = pool;
            _gameDebug = gameDebug;
            _blankTilesSetup = blankTilesSetup;
        }
    }
}