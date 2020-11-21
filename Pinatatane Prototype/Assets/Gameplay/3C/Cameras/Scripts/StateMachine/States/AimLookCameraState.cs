using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimLookCameraState : State<CameraThirdPersonController>
    {
        public override void OnEnter(CameraThirdPersonController element)
        {
            base.OnEnter(element);
            element.LoadCameraControllerProfile("AimLook");
        }

        public override void OnCurrent(CameraThirdPersonController element)
        {
            base.OnCurrent(element);
            element.MoveHorizontal(element.CurrentCameraControllerProfile.rotationSpeed);
        }
    }
}