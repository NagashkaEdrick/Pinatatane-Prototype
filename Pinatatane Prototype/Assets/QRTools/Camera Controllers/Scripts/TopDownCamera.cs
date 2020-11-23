using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/Gameplay/CameraControllers/TopDownCamera", fileName = "TopDownCamera")]

    public class TopDownCamera : CameraController
    {
        public float distance = 2f,
            height = 10f,
            angle = 0f,
            smoothTime = 1f;
        public Vector3 refVelocity;

        public override void Init(CameraHandler element)
        {
            refVelocity = Vector3.zero;
        }

        public override void Execute(CameraHandler element)
        {
            Vector3 worldPos = (Vector3.forward * distance) + (Vector3.up * height);
            Vector3 rotateVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;

            Vector3 targetPos = element.target.position;
            targetPos.y = 0;
            Vector3 finalPos = targetPos + rotateVector;

            element.cameraTransform.position = Vector3.SmoothDamp(element.cameraTransform.position, finalPos, ref refVelocity, smoothTime);
            element.cameraTransform.LookAt(targetPos);
        }
    }
}