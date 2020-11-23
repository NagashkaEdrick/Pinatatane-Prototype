using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    public class CameraController : Behaviour<CameraHandler>
    {
        public Vector3 targetOffset = new Vector3();
        public Vector3 cameraOffset = new Vector3();

        public float lerpCameraFollowingSpeed = 0.02f;
        public float lerpTargetFollowingSpeed = 0.02f;
        
        public override void Execute(CameraHandler element)
        {
            
        }

        public override void Init(CameraHandler element)
        {

        }

        protected void FollowTarget(CameraHandler element)
        {
            element.handler.transform.position = Vector3.Lerp(element.handler.transform.position, element.target.transform.position, lerpTargetFollowingSpeed);
            element.cameraTransform.transform.position = Vector3.Lerp(element.cameraTransform.transform.position, element.target.position + cameraOffset, lerpCameraFollowingSpeed);
        }

        protected void LookTarget(CameraHandler element)
        {
            Vector3 targetPos = element.target.position + targetOffset;
            element.cameraTransform.transform.LookAt(targetPos);
        }

        protected void Look(CameraHandler element)
        {
            Vector3 dir = element.cameraTransform.position - element.target.position;
            dir.Normalize();
            element.cameraTransform.rotation = Quaternion.LookRotation(dir); 
        }
    }
}