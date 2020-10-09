using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;
using UnityEngine;

namespace Pinatatane
{
    public class StateMachine
    {
        public State[] states;
        public int currentStateIndex;
        public bool loop;

        public StateMachine(State[] _states, bool _loop = true)
        {
            loop = _loop;
            CopyStates(_states);
            StartStateMachine();
        }

        public void StartStateMachine()
        {
            currentStateIndex = 0;
            states[currentStateIndex].onStateEnter?.Invoke();
            if (states[currentStateIndex].testConditionAtStart) 
                states[currentStateIndex].TestCondition();
            Debug.Log("Start => " + states[currentStateIndex].stateName);
        }

        public void TestCurrentStateCondition()
        {
            if(states[currentStateIndex].TestCondition() == true)
            {
                states[currentStateIndex]?.onStateExit?.Invoke();
                if (states[currentStateIndex].testConditionAtStart) 
                    states[currentStateIndex].TestCondition();
                Debug.Log("End => " + states[currentStateIndex].stateName);

                currentStateIndex++;
                if(currentStateIndex <= states.Length - 1)
                {
                    states[currentStateIndex]?.onStateEnter?.Invoke();
                    Debug.Log("Start => " + states[currentStateIndex].stateName);
                }

                if (currentStateIndex >= states.Length)
                {
                    if (loop) StartStateMachine();
                    return;
                }
            }
        }

        void CopyStates(State[] _states)
        {
            states = new State[_states.Length];

            for (int i = 0; i < _states.Length; i++)
                states[i] = _states[i];
        }
    }
}