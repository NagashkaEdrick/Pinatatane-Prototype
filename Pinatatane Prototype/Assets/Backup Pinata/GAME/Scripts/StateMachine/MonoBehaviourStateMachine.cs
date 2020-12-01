using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class MonoBehaviourStateMachine : MonoBehaviour
    {
        [SerializeField, ReadOnly, BoxGroup("Debug")] string currentStateName;

        public MonoBehaviourState[] states;
        public MonoBehaviourState currentState;

        int index;

        public void StartStateMachine()
        {
            index = 0;

            if(states != null && states.Length > 0)
                currentState = states[index];
        }

        private void Update()
        {
            CheckCurrentState();
        }

        public void CheckCurrentState()
        {
            if (currentState != null)
            {
                if (currentState?.CheckConditions() == true)
                {
                    NextState();
                }
            }
        }

        public void NextState()
        {
            currentState?.onExit?.Invoke();
            index++;
            if (index < states.Length)
            {
                currentState = states[index];
                currentState?.onEnter?.Invoke();
                currentStateName = currentState.stateName;
            }
        }

        public void PreviousState()
        {
            currentState?.onExit?.Invoke();
            index++;
            if (index < states.Length)
            {
                currentState = states[index];
                currentState?.onEnter?.Invoke();
                currentStateName = currentState.stateName;
            }
        }
    }
}