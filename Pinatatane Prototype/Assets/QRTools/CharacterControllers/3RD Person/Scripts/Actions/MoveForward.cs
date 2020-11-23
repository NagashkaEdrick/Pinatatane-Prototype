using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Inputs;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/PlayerControllers/Actions/MoveForward")]
    public class MoveForward : Behaviour<Transform>
    {
        public float movementSpeed = 2f;
        public QInputAxis
            horizontal = default,
            vertical = default;

        public override void Execute(Transform element)
        {
            Vector3 targetVelocity = element.forward * movementSpeed * (Mathf.Clamp01(Mathf.Abs(horizontal.JoystickValue) + Mathf.Abs(vertical.JoystickValue))) * Time.deltaTime;
            element.transform.position += targetVelocity;
        }

        public override void Init(Transform element)
        {

        }
    }
}