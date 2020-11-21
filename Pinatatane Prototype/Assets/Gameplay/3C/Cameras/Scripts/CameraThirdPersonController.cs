using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class CameraThirdPersonController : CameraController
    {
        private void Start()
        {
            m_stateMachineCameraController?.StartStateMachine(m_stateMachineCameraController.currentState, this);
        }

        public override void CameraUpdate()
        {
            base.CameraUpdate();

            if (m_CurrentCameraControllerProfile == null)
                throw new System.Exception(string.Format("There is no profile in : {0}", this.ToString()));

            m_stateMachineCameraController?.CheckCurrentState(this);
            m_stateMachineCameraController?.currentState?.OnCurrent(this);
            
            LookTarget();
            FollowTarget();
            CameraOffset();
        }

        /// <summary>
        /// Move Horizontal with Joystick.
        /// </summary>
        public void MoveHorizontal(float _speed)
        {
            if (CurrentCameraControllerProfile.blockRotationInX) return;

            angleH += Input.GetAxis("RotationX") * _speed * CurrentCameraControllerProfile.cameraSensibilityX * Time.deltaTime;
            angleH %= 360;

            HandlerTransform.localRotation = Quaternion.Euler(HandlerTransform.localRotation.eulerAngles.x, angleH, HandlerTransform.localRotation.eulerAngles.z);
        }

        /// <summary>
        /// Mover Verticaly with Joystick.
        /// </summary>
        public void MoveVertical(float _speed)
        {
            if (CurrentCameraControllerProfile.blockRotationInY) return;

            angleV += Input.GetAxis("RotationY") * _speed * CurrentCameraControllerProfile.cameraSensibilityY * Time.deltaTime;
            angleV %= 360;

            angleV = Mathf.Clamp(angleV, CurrentCameraControllerProfile.clamp_RotationY.x, CurrentCameraControllerProfile.clamp_RotationY.y);
            HandlerTransform.localRotation = Quaternion.Euler(angleV, HandlerTransform.localRotation.eulerAngles.y, HandlerTransform.localRotation.eulerAngles.z);
        }
    }
}