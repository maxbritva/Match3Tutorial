using Audio;
using UnityEngine;

namespace GameStateMachine.States
{
    public class WinState : IState
    {
        private AudioManager _audioManager;

        public WinState(AudioManager audioManager) => _audioManager = audioManager;

        public void Enter()
        {
            _audioManager.PlayWin();
        }

        public void Exit()
        { }
    }
}