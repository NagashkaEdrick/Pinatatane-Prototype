using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimTriggerUpCameraCondition : Condition<CameraManager>
    {
        public override bool CheckCondition(CameraManager element)
        {
            return Input.GetAxis("LeftTrigger") < .2f;
        }
    }
}