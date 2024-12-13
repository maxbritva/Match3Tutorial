using Game.Board;
using UnityEngine;

namespace GameStateMachine.States
{
    public class PrepareState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private GameBoard _gameBoard;

        public PrepareState(IStateSwitcher stateSwitcher, GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            _stateSwitcher = stateSwitcher;
        }

        public void Enter()
        {
            _gameBoard.CreateBoard();
            _stateSwitcher.SwitchState<PlayerTurnState>();
        }

        public void Exit() => Debug.Log("Game was started!");
    }
}