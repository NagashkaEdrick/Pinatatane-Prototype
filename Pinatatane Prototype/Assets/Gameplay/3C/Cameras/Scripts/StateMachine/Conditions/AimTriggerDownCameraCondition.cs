using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimTriggerDownCameraCondition : Condition<CameraManager>
    {
        public override bool CheckCondition(CameraManager element)
        {
            return Input.GetAxis("LeftTrigger") > .8f;
        }
    }
}