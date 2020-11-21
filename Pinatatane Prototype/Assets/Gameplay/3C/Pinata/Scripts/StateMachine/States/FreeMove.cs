using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class FreeMove : State<PinataController>
    {
        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);

            element.MoveForward();
            element.RotationBasedOnCameraOrientation();
        }        
    }
}