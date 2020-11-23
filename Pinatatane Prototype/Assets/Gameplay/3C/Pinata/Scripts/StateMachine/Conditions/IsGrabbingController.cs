using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class IsGrabbingController : Condition<PinataController>
    {
        public override bool CheckCondition(PinataController element)
        {
            return element.Pinata.lasso.CurrenObjectGrabbed != null;
        }
    }
}
