using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Vinh
{
    public class StateMachine 
    {
        public IState[] states;
        public Enemy agent;
        public StateID currentState;

        public StateMachine(Enemy agent)
        {
            this.agent = agent;
            int numStates = System.Enum.GetNames(typeof(StateID)).Length;
            states = new IState[numStates];
        }

        public void RegisterState(IState state)
        {
            int index = (int)state.GetId();
            states[index] = state;
        }

        public IState GetState(StateID stateID)
        {
            int index = (int)stateID;
            return states[index];
        }
        public void Update()
        {
            GetState(currentState)?.Execute(agent);
        }

        public void ChangeState(StateID newState)
        {
            GetState(currentState)?.Exit(agent);
            currentState = newState;
            GetState(currentState)?.Enter(agent);
        }
    }

    public enum StateID
    {
        Idle,
        Seeking,
        Getting,
        Falling,
        BridgeBuilding,
    }

}
