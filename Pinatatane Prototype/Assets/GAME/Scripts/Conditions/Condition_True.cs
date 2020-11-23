using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    [CreateAssetMenu(menuName = "Pinatatane/Conditions/Return True", fileName ="Return True")]
    public class Condition_True : Condition
    {
        [SerializeField] bool valueToReturn = true;

        public override bool TestCondition()
        {
            return valueToReturn;
        }
    }
}