using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    [System.Serializable]
    public class State
    {
        public string stateName;
        [TextArea(3, 5), SerializeField] string stateDescription; 

        public UnityEvent
            onStateEnter,
            onStateExit,
            onConditionsValidate;

        public Condition[] conditions;

        public bool testConditionAtStart = true;

        public virtual bool TestCondition()
        {
            if(conditions == null || conditions.Length == 0)
            {
                Debug.LogError("ERROR CONDITION IN STATE : " + stateName + " -> Pas de condition(s).");
                return false;
            }

            for (int i = 0; i < conditions.Length; i++)
            {
                if (conditions[i].TestCondition() == false)
                    return false;
            }

            onConditionsValidate?.Invoke();
            return true;
        }
    }
}