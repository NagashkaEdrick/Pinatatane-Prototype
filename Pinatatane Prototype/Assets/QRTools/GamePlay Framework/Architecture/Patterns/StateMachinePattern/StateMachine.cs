using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameplayFramework
{
    public class StateMachine<T> : SerializedMonoBehaviour
    {
        public State<T> currentState;  

        /// <summary>
        /// Start the stateMachine.
        /// </summary>
        public void StartStateMachine(State<T> state, T element)
        {
            if(state != null) currentState = state;
            currentState.OnEnter(element);
        }
        
        /// <summary>
        /// Check the currentState and change state if a condition is validate.
        /// </summary>
        public void CheckCurrentState(T element)
        {
            if(currentState.TryGetNextState(element))
            {
                currentState.OnExit(element);
                currentState.TryGetNextState(element, this);
                currentState.OnEnter(element);
            }           
        }

        /// <summary>
        /// Change the current state witch check conditions.
        /// </summary>
        public void ChangeCurrentState(State<T> newState, T element)
        {
            currentState.OnExit(element);
            currentState = newState;
            currentState.OnEnter(element);
        }
    }
}