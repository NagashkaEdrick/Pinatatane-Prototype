using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class LassoHasGrabbedSomething : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            return element.Lasso.CurrenObjectGrabbed != null;
        }
    }
}