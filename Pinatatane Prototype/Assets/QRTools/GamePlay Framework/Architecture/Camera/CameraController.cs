using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace GameplayFramework
{
    public class CameraController : SerializedMonoBehaviour
    {
        [SerializeField, BoxGroup("Transform Reference")] 
        protected Transform m_HandlerTransform;
        [SerializeField, BoxGroup("Transform Reference")]
        protected Transform m_VirualCameraTransform;
        [SerializeField, BoxGroup("Transform Reference")]
        protected Transform m_TargetTransform;

        [HideInInspector]public Camera Camera;

        [SerializeField] protected CameraControllerProfile m_CurrentCameraControllerProfile;

        #region Properties
        public Transform TargetTransform { get => m_TargetTransform; set => m_TargetTransform = value; }
        public Transform HandlerTransform { get => m_HandlerTransform; set => m_HandlerTransform = value; }
        public Transform VirtualCameraTransform { get => m_VirualCameraTransform; set => m_VirualCameraTransform = value; }
        public CameraControllerProfile CurrentCameraControllerProfile { get => m_CurrentCameraControllerProfile; set => m_CurrentCameraControllerProfile = value; }
        #endregion    

        [SerializeField] Dictionary<string, CameraControllerProfile> m_CameraControllerProfiles = new Dictionary<string, CameraControllerProfile>();

        protected Vector3 targetPos;

        [HideInInspector]
        public float
            angleH,
            angleV;

        private void Start()
        {
            Camera = CameraManager.Instance.MainCamera;
        }

        /// <summary>
        /// Update camera in runtime.
        /// </summary>
        public virtual void CameraUpdate()
        {
            PositionCameraOnVirtualCamera();
        }
        
        /// <summary>
        /// Paint the position of the main camera on the virtual camera.
        /// </summary>
        public void PositionCameraOnVirtualCamera()
        {
            if (CameraManager.Instance.inTransition)
                return;

            Camera.transform.position = m_VirualCameraTransform.position;
            Camera.transform.rotation = m_VirualCameraTransform.rotation;
        }

        /// <summary>
        /// Camera rotation look the target.
        /// </summary>
        public void LookTarget()
        {
            targetPos = TargetTransform.position + CurrentCameraControllerProfile.lookTargetOffset;
            Camera.transform.LookAt(targetPos);
        }

        /// <summary>
        /// Add an offset to the camera transform
        /// </summary>
        public void CameraOffset()
        {
            VirtualCameraTransform.localPosition = Vector3.Lerp(
                VirtualCameraTransform.localPosition,
                CurrentCameraControllerProfile.positionOffset,
                CurrentCameraControllerProfile.lerpFollowingOffsetSpeed);
        }

        /// <summary>
        /// Camera Handler follow the target
        /// </summary>
        public void FollowTarget()
        {
            HandlerTransform.transform.position = Vector3.Lerp(
                HandlerTransform.transform.position,
                TargetTransform.transform.position,
                CurrentCameraControllerProfile.lerpFollowingSpeed);
        }

        /// <summary>
        /// Copy angles values.
        /// </summary>
        public void CopyAnglesValues(CameraController from, CameraController to)
        {
            to.angleH = from.angleH;
            to.angleV = from.angleV;
        }

        /// <summary>
        /// Find a profile in the dictionary m_CameraControllerProfiles and load it in m_CurrentCameraControllerProfile.
        /// </summary>
        public CameraControllerProfile LoadCameraControllerProfile(string profileName)
        {
            m_CameraControllerProfiles.TryGetValue(profileName, out var profile);
            if (profile == null)
                throw new System.Exception(
                    string.Format("Profile \"{0}\" not founded in {1}.",
                    profileName,
                    m_CameraControllerProfiles.ToString())
                    );
            m_CurrentCameraControllerProfile = profile;
            return profile;
        }
    }
}