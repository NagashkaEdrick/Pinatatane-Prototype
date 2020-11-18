using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// This class is used to control the pinata.
    /// </summary>
    public class PinataController : PlayerController
    {
        [SerializeField] Pinata m_pinata = default;

        float horizontal, vertical, moveAmount;

        [SerializeField] Transform cameraTransform = default;

        public override void Control(IPawn pawn)
        {
            MoveForward(pawn);
            RotationBasedOnCameraOrientation(pawn);

            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    pawn.PawnTransform.position += Vector3.forward * m_pinata.PinataData.movementSpeed * Time.deltaTime;
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    pawn.PawnTransform.position -= Vector3.forward * m_pinata.PinataData.movementSpeed * Time.deltaTime;
            //}
        }

        void MoveForward(IPawn pawn)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            Vector3 targetVelocity = pawn.PawnTransform.forward * m_pinata.PinataData.movementSpeed * moveAmount * Time.deltaTime;
            pawn.PawnTransform.position += targetVelocity;
        }

        void RotationBasedOnCameraOrientation(IPawn pawn)
        {
            Vector3 targetDir = cameraTransform.forward * vertical;
            targetDir += cameraTransform.right * horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = pawn.PawnTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(pawn.PawnTransform.rotation, tr, Time.deltaTime * moveAmount * m_pinata.PinataData.movementSpeed);

            pawn.PawnTransform.rotation = targetRot;
        }
    }
}