using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class GrabbingMovement : State<PinataController>
    {
        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);

            element.AimMovement(element.Pinata.PinataData.grabMovementSpeed);
            element.AimRotation();
        }
    }
}