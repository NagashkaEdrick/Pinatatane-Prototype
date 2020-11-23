using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class IsBlocked : Condition<PinataController>
    {
        [SerializeField] bool WaitingValue = true;

        public override bool CheckCondition(PinataController element)
        {
            return element.IsBlocked == WaitingValue;
        }
    }
}