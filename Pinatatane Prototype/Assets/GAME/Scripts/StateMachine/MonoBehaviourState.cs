using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pinatatane
{
    public class MonoBehaviourState : MonoBehaviour
    {
        public string stateName;

        public UnityEvent onEnter, onExit;

        public MonoBehaviourCondition[] conditions;

        public bool CheckConditions()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].IsValidate())
                    return false;
            }

            return true;
        }
    }
}