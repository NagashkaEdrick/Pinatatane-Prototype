using QRTools.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools
{
    [CreateAssetMenu(menuName = "QRTools/Gameplay/CameraControllers/ThirdPersonCamera", fileName = "ThirdPersonCamera")]
    public class ThirdPersonCamera : CameraController
    {
        [SerializeField] QInputAxis horizontal, vertical;        
               
        public float speed = 5;
        public Vector2 clampV = new Vector2(-45,45);


        public override void Init(CameraHandler element)
        {
        }

        public override void Execute(CameraHandler element)
        {
            FollowTarget(element);
            LookTarget(element);
            MoveHorizontal(element);
            MoveVertical(element);
        }

        void MoveHorizontal(CameraHandler element)
        {
            element.angleH += vertical.JoystickValue * speed * Time.deltaTime;
            element.handler.localRotation = Quaternion.Euler(element.handler.localRotation.eulerAngles.x, element.angleH, element.handler.localRotation.eulerAngles.z);
        }

        void MoveVertical(CameraHandler element)
        {
            element.angleV += horizontal.JoystickValue * speed * Time.deltaTime;

            element.angleV = Mathf.Clamp(element.angleV, clampV.x, clampV.y);

            element.handler.localRotation = Quaternion.Euler(element.angleV, element.handler.localRotation.eulerAngles.y, element.handler.localRotation.eulerAngles.z);
        }
    }
}