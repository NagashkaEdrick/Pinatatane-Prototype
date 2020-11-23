using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimAndRightTriggerDownCondition : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            return InputManager.Instance.aimButton.IsTrigger && InputManager.Instance.grabButton.IsTrigger;
        }
    }
}