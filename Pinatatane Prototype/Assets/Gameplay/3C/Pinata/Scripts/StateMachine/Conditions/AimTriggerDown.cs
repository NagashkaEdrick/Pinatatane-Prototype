using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace Pinatatane
{
    public class AimTriggerDown : Condition<PinataController>
    {
        public override bool CheckCondition(PinataController element)
        {
            if(Input.GetAxis("LeftTrigger") > .8f)
                return true;

            return false;
        }
    }
}