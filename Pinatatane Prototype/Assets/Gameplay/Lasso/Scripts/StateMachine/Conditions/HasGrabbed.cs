using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class HasGrabbed : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            return element.hasGrabbed;
        }
    }
}