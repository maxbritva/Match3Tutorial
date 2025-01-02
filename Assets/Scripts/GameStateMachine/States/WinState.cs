using Audio;
using Game.UI;
using UnityEngine;

namespace GameStateMachine.States
{
    public class WinState : IState
    {
        private EndGamePanelView _endGame;

        public WinState(EndGamePanelView endGame) => _endGame = endGame;

        public void Enter() => _endGame.ShowEndGamePanel(true);

        public void Exit()
        { }
    }
}