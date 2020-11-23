using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/Gameplay/CharacterController/TopDownController", fileName = "TopDownController")]
    public class TopDownController : CharacterController
    {
        public MoveForward moveForward = default;
        public RotateBasedOnMousePosition rotateBasedOnMousePosition = default;

        public override void Execute(PlayerController element)
        {
            moveForward.Execute(element.transform);
            rotateBasedOnMousePosition.Execute(element.transform);
        }

        public override void Init(PlayerController element)
        {
            moveForward.Init(element.transform);
            rotateBasedOnMousePosition.Init(element.transform);
        }
    }
}