using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OldPinatatane
{
    public class AlwaysTrue : MonoBehaviourCondition
    {
        public override bool IsValidate()
        {
            return true;
        }
    }
}