using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Vinh
{
    public class GettingState : IState
    {
        NavMeshAgent navMeshAgent;
        int brickCount;
        public void Enter(Enemy agent)
        {
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(agent.Destination);
            brickCount = agent.GetBrickCount();
        }

        public void Execute(Enemy agent)
        {
            if (brickCount >= agent.BridgeLimit)
            {
                agent.ChangeState(StateID.BridgeBuilding);
            }



            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                agent.ChangeState(StateID.Seeking);
            }
        }

        public void Exit(Enemy agent)
        {

        }

        public StateID GetId()
        {
            return StateID.Getting;
        }
    }
}

