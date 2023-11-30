using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Vinh
{
    public class IdleState : IState
    {
        const float PREPARE_TIME = 2f;


        float timer = 0;
        public void Enter(Enemy agent)
        {
        }

        public void Execute(Enemy agent)
        {
            timer += Time.deltaTime;
            if (timer >= PREPARE_TIME)
            {
                agent.stateMachine.ChangeState(StateID.Seeking);
            }
        }

        public void Exit(Enemy agent)
        {
            
        }

        public StateID GetId()
        {
            return StateID.Idle;
        }
    }
}

