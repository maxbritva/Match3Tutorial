using Audio;
using UnityEngine;

namespace GameStateMachine.States
{
    public class LooseState : IState
    {
        private AudioManager _audioManager;

        public LooseState(AudioManager audioManager) => _audioManager = audioManager;

        public void Enter()
        {
            _audioManager.PlayLoose();
        }
        public void Exit()
        { }
    }
}