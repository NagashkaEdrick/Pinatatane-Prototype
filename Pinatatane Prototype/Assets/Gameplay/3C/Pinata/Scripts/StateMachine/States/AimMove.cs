using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using System;

namespace Pinatatane
{
    public class AimMove : State<PinataController>
    {
        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);

            element.AimMovement(element.Pinata.PinataData.aimMovementSpeed);
            element.AimRotation();
        }
    }
}