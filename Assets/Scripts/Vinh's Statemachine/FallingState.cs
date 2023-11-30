using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Vinh
{
    public class FallingState : IState
    {
        NavMeshAgent navMeshAgent;
        float timer = 0f;
        float agentSpeed;
        const float FALLING_TIME = 1f;
        public void Enter(Enemy agent)
        {
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            agent.GetComponent<Animator>().SetTrigger("Fall");
            agentSpeed = navMeshAgent.speed;
            navMeshAgent.speed = 0f;
        }

        public void Execute(Enemy agent)
        {
            timer += Time.deltaTime;
            if (timer >= FALLING_TIME)
            {
                agent.ChangeState(StateID.Seeking);
            }
        }

        public void Exit(Enemy agent)
        {
            agent.GetComponent<NavMeshAgent>().speed = agentSpeed;
        }

        public StateID GetId()
        {
            return StateID.Falling;
        }
    }

}
