using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class CameraThirdPersonController : CameraController
    {
        public override void CameraUpdate()
        {
            base.CameraUpdate();

            if (m_CurrentCameraControllerProfile == null)
                throw new System.Exception(string.Format("There is no profile in : {0}", this.ToString()));

            MoveHorizontal(Input.GetAxis("RotationX"));
            MoveVertical(Input.GetAxis("RotationY"));

            LookTarget();
            FollowTarget();
            CameraOffset();
        }

        /// <summary>
        /// Move Horizontal in function of the handler.
        /// </summary>
        public void MoveHorizontal(float normalizeSpeed)
        {
            if (CurrentCameraControllerProfile.blockRotationInX) return;

            angleH += normalizeSpeed * m_CurrentCameraControllerProfile.rotationSpeed * CurrentCameraControllerProfile.cameraSensibilityX * Time.deltaTime;
            angleH %= 360;

            HandlerTransform.localRotation = Quaternion.Euler(HandlerTransform.localRotation.eulerAngles.x, angleH, HandlerTransform.localRotation.eulerAngles.z);
        }

        /// <summary>
        /// Mover Verticaly in function of the handler.
        /// </summary>
        public void MoveVertical(float normalizeSpeed)
        {
            if (CurrentCameraControllerProfile.blockRotationInY) return;

            angleV += normalizeSpeed * m_CurrentCameraControllerProfile.rotationSpeed * CurrentCameraControllerProfile.cameraSensibilityY * Time.deltaTime;
            angleV %= 360;

            angleV = Mathf.Clamp(angleV, CurrentCameraControllerProfile.clamp_RotationY.x, CurrentCameraControllerProfile.clamp_RotationY.y);
            HandlerTransform.localRotation = Quaternion.Euler(angleV, HandlerTransform.localRotation.eulerAngles.y, HandlerTransform.localRotation.eulerAngles.z);
        }
    }
}