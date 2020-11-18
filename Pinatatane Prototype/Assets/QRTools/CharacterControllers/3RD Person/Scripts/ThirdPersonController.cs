using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/Gameplay/CharacterController/ThirdPersonController", fileName = "ThirdPersonController")]
    public class ThirdPersonController : CharacterController
    {
        public MoveForward moveForward;
        public RotateBasedOnCameraOrientation rotateBasedOnCameraOrientation;

        public override void Execute(PlayerController element)
        {
            moveForward.Execute(element.transform);
            rotateBasedOnCameraOrientation.Execute(element);
        }

        public override void Init(PlayerController element)
        {
        }
    }
}