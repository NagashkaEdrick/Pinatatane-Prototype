using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pinatatane;

namespace GameplayFramework
{
    public class CameraThirdPersonController : MonoBehaviour
    {
        [SerializeField] Transform m_HandlerTransform;
        [SerializeField] Transform m_CameraTransform;
        [SerializeField] Transform m_TargetTransform;

        [SerializeField] CameraControllerData cameraControllerData;

        [SerializeField] StateMachineCameraController m_stateMachineCameraController;

        public bool
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

        public Transform HandlerTransform { get => m_HandlerTransform; set => m_HandlerTransform = value; }
        public Transform CameraTransform { get => m_CameraTransform; set => m_CameraTransform = value; }
        public Transform TargetTransform1 { get => m_TargetTransform; set => m_TargetTransform = value; }
        public CameraControllerData CameraControllerData { get => cameraControllerData; set => cameraControllerData = value; }
        #endregion    

        Vector3 targetPos;

        private void Start()
        {
            m_stateMachineCameraController.StartStateMachine(m_stateMachineCameraController.currentState, this);
        }

        private void Update()
        {
            m_stateMachineCameraController.CheckCurrentState(this);
            m_stateMachineCameraController.currentState?.OnCurrent(this);
            
            LookTarget();
            FollowTarget();
            CameraOffset();
        }


        void LookTarget()
        {
            targetPos = TargetTransform.position + CameraControllerData.lookTargetOffset;
            CameraTransform.transform.LookAt(targetPos);
        }

        void CameraOffset()
        {
            CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, CameraControllerData.positionOffset, CameraControllerData.lerpFollowingOffsetSpeed);
        }

        void FollowTarget()
        {
            HandlerTransform.transform.position = Vector3.Lerp(HandlerTransform.transform.position, TargetTransform.transform.position, CameraControllerData.lerpFollowingSpeed);
        }

    }
}