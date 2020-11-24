using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class LassoHasGrabbedNothing : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            if (!element.isConstructed)
                return false;

            return element.Lasso.CurrenObjectGrabbed == null;
        }
    }
}