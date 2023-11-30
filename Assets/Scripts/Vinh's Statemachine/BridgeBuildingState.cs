using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Vinh
{
    public class BridgeBuildingState : IState
    {
        NavMeshAgent navMeshAgent;
        public void Enter(Enemy agent)
        {
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(agent.GoalPosition);
            
        }

        public void Execute(Enemy agent)
        {
            if (agent.GetBrickCount() == 0)
            {
                agent.ChangeState(StateID.Seeking);
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
            return StateID.BridgeBuilding;
        }
    }
}
