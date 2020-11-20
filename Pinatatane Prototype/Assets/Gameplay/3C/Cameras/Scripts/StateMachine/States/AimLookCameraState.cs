using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class AimLookCameraState : State<CameraThirdPersonController>
    {
        public override void OnCurrent(CameraThirdPersonController element)
        {
            base.OnCurrent(element);

            element.CameraTransform.forward = Vector3.Lerp(
                element.CameraTransform.forward,
                new Vector3(
                    element.TargetTransform.forward.x,
                    element.CameraTransform.forward.y,
                    element.TargetTransform.forward.z),
                .2f
                );
        }
    }
}