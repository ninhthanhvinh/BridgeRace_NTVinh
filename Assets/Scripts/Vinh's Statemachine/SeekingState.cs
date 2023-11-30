using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinh
{
    public class SeekingState : IState
    {
        public void Enter(Enemy agent)
        {
            agent.Destination = agent.GetBrickPosition();
            agent.ChangeState(StateID.Getting);
        }

        public void Exit(Enemy agent)
        {
        }

        public StateID GetId()
        {
            return StateID.Seeking;
        }

        public void Execute(Enemy agent)
        {
            
        }
    }
}

