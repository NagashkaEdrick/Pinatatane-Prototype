using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class LeftCondition : Condition<LassoController>
    {
        public override bool CheckCondition(LassoController element)
        {
            return InputManager.Instance.cameraRotX.JoystickValue < -.95f;
        }
    }
}