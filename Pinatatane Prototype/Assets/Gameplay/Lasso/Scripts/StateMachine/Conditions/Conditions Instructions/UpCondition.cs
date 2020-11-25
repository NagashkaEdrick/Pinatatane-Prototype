using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class UpCondition : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            return InputManager.Instance.moveY.JoystickValue > .95f;
        }
    }
}