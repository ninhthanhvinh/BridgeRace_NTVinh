using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinh
{
    public interface IState 
    {
        public StateID GetId();
        void Enter(Enemy agent);
        void Execute(Enemy agent);
        void Exit(Enemy agent);
    }

}
