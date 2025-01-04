using Game.Board;
using Game.Tiles;
using Levels;
using UnityEngine;

namespace GameStateMachine.States
{
    public class PrepareState : IState
    {
        private BackgroundTilesSetup _backgroundTilesSetup;
        private BlankTilesSetup _blankTilesSetup;
        private readonly IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;
        private LevelConfig _levelConfig;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard, LevelConfig levelConfig,
            BackgroundTilesSetup backgroundTilesSetup, BlankTilesSetup blankTilesSetup)
        {
            _gameBoard = gameBoard;
            _levelConfig = levelConfig;
            _stateSwitcher = stateSwitcher;
            _blankTilesSetup = blankTilesSetup;
            _backgroundTilesSetup = backgroundTilesSetup;
        }

        public async void Enter()
        {
            await _backgroundTilesSetup.SetupBackground(_gameBoard.transform, _blankTilesSetup.Blanks,
                _levelConfig.Width, _levelConfig.Height);
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started!");
    }
}