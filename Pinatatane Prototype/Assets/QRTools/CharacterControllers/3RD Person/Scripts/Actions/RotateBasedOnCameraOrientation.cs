using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/PlayerControllers/Actions/Rotate Based On Camera Orientation")]
    public class RotateBasedOnCameraOrientation : Behaviour<PlayerController>
    {
        public float speed = 8;
        public QInputAxis
            horizontal,
            vertical;

        public override void Execute(PlayerController element)
        {
            float h = horizontal.JoystickValue;
            float v = vertical.JoystickValue;

            Vector3 targetdir = element.cameraHandler.cameraTransform.forward * v;
            targetdir += element.cameraHandler.cameraTransform.right * h;
            targetdir.Normalize();
            targetdir.y = 0;

            if (targetdir == Vector3.zero)
                targetdir = element.transform.forward;

            Quaternion tr = Quaternion.LookRotation(targetdir);
            Quaternion targetrot = Quaternion.Slerp(
                element.transform.rotation,
                tr,
                Time.deltaTime * speed * Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v)));

            element.transform.rotation = targetrot;
        }

        public override void Init(PlayerController element)
        {

        }
    }
}