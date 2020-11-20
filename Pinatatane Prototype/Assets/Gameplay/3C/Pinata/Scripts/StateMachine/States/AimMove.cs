using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using System;

namespace Pinatatane
{
    public class AimMove : State<PinataController>
    {
        private float horizontal;
        private float vertical;
        private float moveAmount;

        public override void OnCurrent(PinataController element)
        {
            base.OnCurrent(element);
            AimMovement(element);
        }

        private void AimMovement(PinataController element)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 movementVector = new Vector3(horizontal, 0, vertical) * element.Pinata.PinataData.movementLateralSpeed;

            element.Pawn.PawnTransform.position += movementVector * Time.deltaTime;

            //element.cameraTransform.forward = Vector3.Lerp(
            //    element.cameraTransform.forward,
            //    new Vector3(element.Pawn.PawnTransform.forward.x, element.cameraTransform.forward.y, element.Pawn.PawnTransform.forward.z),
            //    .2f
            //    );
        }
    }
}