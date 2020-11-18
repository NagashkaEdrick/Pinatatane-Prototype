using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public class CameraThirdPersonController : MonoBehaviour
    {
        [SerializeField] Transform m_HandlerTransform;
        [SerializeField] Transform m_CameraTransform;
        [SerializeField] Transform m_TargetTransform;

        [SerializeField] CameraControllerData cameraControllerData;

        [SerializeField]
        bool
            blockRotationInX = false,
            blockRotationInY = false;

        #region Properties
        public Transform TargetTransform { get => m_TargetTransform; set => m_TargetTransform = value; }

        public bool BlockRotationInX
        {
            get => blockRotationInX;
            set
            {
                blockRotationInX = value;
            }
        }

        public bool BlockRotationInY
        {
            get => blockRotationInY;
            set
            {
                blockRotationInY = value;
            }
        }
        #endregion

        float angleH;
        float angleV;      

        Vector3 targetPos;

        private void Update()
        {
            LookTarget();
            FollowTarget();
            CameraOffset();
            if(!blockRotationInY) MoveVertical();
            if(!blockRotationInX) MoveHorizontal();
        }

        void LookTarget()
        {
            targetPos = m_TargetTransform.position + cameraControllerData.lookTargetOffset;
            m_CameraTransform.transform.LookAt(targetPos);
        }

        void CameraOffset()
        {
            m_CameraTransform.localPosition = Vector3.Lerp(m_CameraTransform.localPosition, cameraControllerData.positionOffset, cameraControllerData.lerpFollowingOffsetSpeed);
        }

        void FollowTarget()
        {
            m_HandlerTransform.transform.position = Vector3.Lerp(m_HandlerTransform.transform.position, m_TargetTransform.transform.position, cameraControllerData.lerpFollowingSpeed);
        }

        void MoveHorizontal()
        {
            angleH += Input.GetAxis("RotationX") * cameraControllerData.movementSpeed * cameraControllerData.cameraSensibilityX * Time.deltaTime;
            angleH %= 360;

            m_HandlerTransform.localRotation = Quaternion.Euler(m_HandlerTransform.localRotation.eulerAngles.x, angleH, m_HandlerTransform.localRotation.eulerAngles.z);
        }

        void MoveVertical()
        {
            angleV += Input.GetAxis("RotationY") * cameraControllerData.movementSpeed * cameraControllerData.cameraSensibilityY * Time.deltaTime;
            angleV %= 360;

            angleV = Mathf.Clamp(angleV, cameraControllerData.clamp_RotationY.x, cameraControllerData.clamp_RotationY.y);
            m_HandlerTransform.localRotation = Quaternion.Euler(angleV, m_HandlerTransform.localRotation.eulerAngles.y, m_HandlerTransform.localRotation.eulerAngles.z);
        }
    }
}