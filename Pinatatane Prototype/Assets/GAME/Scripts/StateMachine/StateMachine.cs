using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;
using UnityEngine;

namespace Pinatatane
{
    [System.Serializable]
    public class StateMachine
    {
        public List<State> states = new List<State>();

        public int currentStateIndex;

        public bool loop;

        public StateMachine(bool _loop = false)
        {
            loop = _loop;
        }

        public void StartStateMachine()
        {
            currentStateIndex = 0;
            states[currentStateIndex].onEnter?.Invoke();
        }

        public void NextState()
        {
            states[currentStateIndex].onExit?.Invoke();
            currentStateIndex++;

            if (loop && currentStateIndex == states.Count)
                currentStateIndex = 0;

            states[currentStateIndex].onEnter?.Invoke();
        }
    }

    [System.Serializable]
    public class State
    {
        public UnityEvent
            onEnter,
            onExit;
    }
}