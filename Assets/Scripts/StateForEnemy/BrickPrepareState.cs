using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrickPrepareState : IState<Enemy>
{
    NavMeshAgent agent;
    public void OnEnter(Enemy t)
    {
        agent = t.GetComponent<NavMeshAgent>();
        agent.SetDestination(t.Destination);
    }

    public void OnExecute(Enemy t)
    {
        if (agent.remainingDistance < 1f)
        {
            Debug.Log("BrickPrepareState");
            agent.isStopped = true;
            //t.ChangeState(new SeekingState());
        }
    }

    public void OnExit(Enemy t)
    {
        
    }
}
