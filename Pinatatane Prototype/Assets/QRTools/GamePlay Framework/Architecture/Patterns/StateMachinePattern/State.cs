﻿using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace GameplayFramework
{
    /// <summary>
    /// A State for a generic state machine 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class State<T> : SerializedMonoBehaviour
    {
        [SerializeField, TextArea(3, 5), BoxGroup("State infos")] private string Description = "";

        [BoxGroup("State infos")]
        public Dictionary<Condition<T>, State<T>> nextState = new Dictionary<Condition<T>, State<T>>();

        [FoldoutGroup("Callbacks")]
        public UnityEvent
            onEnter,
            onCurrent,
            onExit;

        /// <summary>
        /// Callback when this state become the current state of the state machine.
        /// </summary>
        public virtual void OnEnter(T element)
        {
            onEnter?.Invoke();
        }

        /// <summary>
        /// Callback call in update of the state machine.
        /// </summary>
        public virtual void OnCurrent(T element)
        {
            onCurrent?.Invoke();
        }

        /// <summary>
        /// Callback when this state change.
        /// </summary>
        public virtual void OnExit(T element)
        {
            onExit?.Invoke();
        }

        /// <summary>
        /// actualize = true -> Change the state in the statemachine
        /// </summary>
        public bool TryGetNextState(T element, StateMachine<T> stateMachineToActualize = null)
        {
            if (nextState == null || nextState.Count == 0)
                return false;

            if (GetNextState(element) == null)
                return false;
            else
            {
                if(stateMachineToActualize != null)
                    stateMachineToActualize.currentState = GetNextState(element);
                return true;
            }
        }

        /// <summary>
        /// Get next state in fonction of conditions
        /// </summary>
        State<T> GetNextState(T element)
        {
            Condition<T> c = null;
            if (CheckCondition(element, out c))
            {
                nextState.TryGetValue(c, out var s);
                return s;
            }

            return null;
        }

        /// <summary>
        /// Check if a condition is validate and out it.
        /// </summary>
        bool CheckCondition(T element, out Condition<T> cond)
        {
            foreach(Condition<T> c in nextState.Keys)
            {
                if (c.CheckCondition(element))
                {
                    cond = c;
                    return true;
                }
            }
            cond = null;
            return false;
        }
    }
}