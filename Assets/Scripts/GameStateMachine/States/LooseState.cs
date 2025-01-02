using Audio;
using Game.UI;
using UnityEngine;

namespace GameStateMachine.States
{
    public class LooseState : IState
    {
        private EndGamePanelView _endGame;

        public LooseState(EndGamePanelView endGame) => _endGame = endGame;

        public void Enter() => _endGame.ShowEndGamePanel(false);
        
        public void Exit()
        { }
    }
}