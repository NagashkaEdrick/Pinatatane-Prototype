using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using System;

namespace Pinatatane
{
    public class AimMove : State<PinataController>
    {
        private float horizontal;
        private float vertical;

        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);

            element.AimMovement();
            element.AimRotation();
        }
    }
}