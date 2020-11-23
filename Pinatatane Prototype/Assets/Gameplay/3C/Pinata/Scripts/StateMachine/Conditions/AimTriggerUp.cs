using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace Pinatatane
{
    public class AimTriggerUp : Condition<PinataController>
    {
        public override bool CheckCondition(PinataController element)
        {
            return InputManager.Instance.aimButton.IsTrigger ? false : true;
        }
    }
}