using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimTriggerUpCameraCondition : Condition<CameraThirdPersonController>
    {
        public override bool CheckCondition(CameraThirdPersonController element)
        {
            return Input.GetAxis("LeftTrigger") < .2f;
        }
    }
}