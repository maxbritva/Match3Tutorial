using System;
using UnityEngine;

namespace GameStateMachine.States
{
    public class RefillGridState: IState, IDisposable
    {
        public void Enter()
        {
          Debug.Log("Refill state started");
        }

        public void Exit()
        {
            
        }

        public void Dispose()
        {
           
        }
    }
}