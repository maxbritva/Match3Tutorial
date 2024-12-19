using UnityEngine;

namespace GameStateMachine.States
{
    public class LooseState : IState
    {
        public void Enter()
        {
            Debug.Log("Win");
        }

        public void Exit()
        { }
    }
}