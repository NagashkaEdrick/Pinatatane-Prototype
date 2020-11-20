using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class BoolConditionPinataController : Condition<PinataController>
    {
        public bool expectedResult = false;

        public override bool CheckCondition(PinataController element)
        {
            return expectedResult;
        }
    }
}