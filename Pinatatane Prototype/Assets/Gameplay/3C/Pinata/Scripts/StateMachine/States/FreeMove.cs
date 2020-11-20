using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    public class FreeMove : State<PinataController>
    {

        float horizontal, vertical, moveAmount;


        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);

            MoveForward(element.Pawn, element);
            RotationBasedOnCameraOrientation(element.Pawn, element);
        }

        /// <summary>
        /// Avancer tout droit.
        /// </summary>
        void MoveForward(IPawn pawn, PinataController controller)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            Vector3 targetVelocity = pawn.PawnTransform.forward * controller.Pinata.PinataData.movementSpeed * moveAmount * Time.deltaTime;
            pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Direction que le pawn doit prendre = Direction de la camera * axes des inputs;
        /// </summary>
        void RotationBasedOnCameraOrientation(IPawn pawn, PinataController controller)
        {
            Vector3 targetDir = controller.cameraTransform.forward * vertical;
            targetDir += controller.cameraTransform.right * horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = pawn.PawnTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(pawn.PawnTransform.rotation, tr, Time.deltaTime * moveAmount * controller.Pinata.PinataData.movementSpeed);

            pawn.PawnTransform.rotation = targetRot;
        }
    }
}