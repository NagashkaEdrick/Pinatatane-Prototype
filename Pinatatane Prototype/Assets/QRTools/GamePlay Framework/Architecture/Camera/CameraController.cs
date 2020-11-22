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
        protected Transform m_CameraTransform;
        [SerializeField, BoxGroup("Transform Reference")]
        protected Transform m_TargetTransform;

        public Camera Camera;

        [SerializeField] protected CameraControllerProfile m_CurrentCameraControllerProfile;

        #region Properties
        public Transform TargetTransform { get => m_TargetTransform; set => m_TargetTransform = value; }
        public Transform HandlerTransform { get => m_HandlerTransform; set => m_HandlerTransform = value; }
        public Transform CameraTransform { get => m_CameraTransform; set => m_CameraTransform = value; }
        public CameraControllerProfile CurrentCameraControllerProfile { get => m_CurrentCameraControllerProfile; set => m_CurrentCameraControllerProfile = value; }
        #endregion    

        [SerializeField] Dictionary<string, CameraControllerProfile> m_CameraControllerProfiles = new Dictionary<string, CameraControllerProfile>();

        protected Vector3 targetPos;

        [HideInInspector]
        public float
            angleH,
            angleV;

        public virtual void CameraUpdate()
        {
            PaintCamera();
        }
        
        public void PaintCamera()
        {
            if (CameraManager.Instance.inTransition)
                return;

            Camera.transform.position = m_CameraTransform.position;
            Camera.transform.rotation = m_CameraTransform.rotation;
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
            CameraTransform.localPosition = Vector3.Lerp(
                CameraTransform.localPosition,
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

        public void CopyAnglesValues(CameraController c)
        {
            angleH = c.angleH;
            angleV = c.angleV;
            //m_HandlerTransform.forward = new Vector3(
            //    m_TargetTransform.forward.x,
            //    m_HandlerTransform.forward.y,
            //    m_TargetTransform.forward.z
            //    );
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