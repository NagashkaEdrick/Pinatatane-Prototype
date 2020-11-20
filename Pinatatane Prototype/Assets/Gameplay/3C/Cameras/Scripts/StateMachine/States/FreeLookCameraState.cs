using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class FreeLookCameraState : State<CameraThirdPersonController>
    {
        float angleH;
        float angleV;

        public override void OnCurrent(CameraThirdPersonController element)
        {
            base.OnCurrent(element);

            if (!element.blockRotationInY) MoveVertical(element);
            if (!element.blockRotationInX) MoveHorizontal(element);
        }

        void MoveHorizontal(CameraThirdPersonController element)
        {
            angleH += Input.GetAxis("RotationX") * element.CameraControllerData.movementSpeed * element.CameraControllerData.cameraSensibilityX * Time.deltaTime;
            angleH %= 360;

            element.HandlerTransform.localRotation = Quaternion.Euler(element.HandlerTransform.localRotation.eulerAngles.x, angleH, element.HandlerTransform.localRotation.eulerAngles.z);
        }

        void MoveVertical(CameraThirdPersonController element)
        {
            angleV += Input.GetAxis("RotationY") * element.CameraControllerData.movementSpeed * element.CameraControllerData.cameraSensibilityY * Time.deltaTime;
            angleV %= 360;

            angleV = Mathf.Clamp(angleV, element.CameraControllerData.clamp_RotationY.x, element.CameraControllerData.clamp_RotationY.y);
            element.HandlerTransform.localRotation = Quaternion.Euler(angleV, element.HandlerTransform.localRotation.eulerAngles.y, element.HandlerTransform.localRotation.eulerAngles.z);
        }
    }
}