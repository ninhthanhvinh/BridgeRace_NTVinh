using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekingState : IState<Enemy>
{
    public void OnEnter(Enemy t)
    {
        
    }

    public void OnExecute(Enemy t)
    {
        Vector3 pos = t.GetBrickPosition();
        if (Vector3.Distance(pos, Vector3.zero) > 0.1f)
        {
            t.Destination = pos;
            //t.ChangeState(new BrickPrepareState());
        }

    }

    public void OnExit(Enemy t)
    {
        
    }
}
