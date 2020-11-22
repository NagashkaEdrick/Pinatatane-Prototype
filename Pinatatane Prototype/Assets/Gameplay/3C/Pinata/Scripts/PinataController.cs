using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using DG.Tweening;

using GameplayFramework;

namespace Pinatatane
{
    /// <summary>
    /// This class is used to control the pinata.
    /// </summary>
    public class PinataController : PlayerController
    {
        [SerializeField] Pinata m_pinata = default;
        public Pinata Pinata { get => m_pinata; set => m_pinata = value; }

        public StateMachinePinataController controllerStateMachine;

        public Transform cameraTransform = default;

        public PlayerInputs playerInputs;

        float 
            horizontal, 
            vertical, 
            moveAmount;

        /// <summary>
        /// Différent de IsControllable car ne bloque pas la lecture de la state machine.
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        public override void OnStart()
        {
            base.OnStart();
            controllerStateMachine.StartStateMachine(controllerStateMachine.currentState ,this);
        }

        public override void Control(IPawn pawn)
        {
            controllerStateMachine.currentState.OnCurrent(this);
            controllerStateMachine.CheckCurrentState(this);
        }

        /// <summary>
        /// Avancer tout droit.
        /// </summary>
        public void MoveForward()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            Vector3 targetVelocity = m_pawn.PawnTransform.forward * m_pinata.PinataData.movementSpeed * moveAmount * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Direction que le pawn doit prendre = Direction de la camera * axes des inputs;
        /// </summary>
        public void RotationBasedOnCameraOrientation()
        {
            Vector3 targetDir = cameraTransform.forward * vertical;
            targetDir += cameraTransform.right * horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = m_pawn.PawnTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(m_pawn.PawnTransform.rotation, tr, Time.deltaTime * moveAmount * Pinata.PinataData.movementSpeed);

            m_pawn.PawnTransform.rotation = targetRot;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void AimMovement()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            Vector3 targetVelocity = m_pawn.PawnTransform.forward * m_pinata.PinataData.movementSpeed * vertical * Time.deltaTime;
            targetVelocity += m_pawn.PawnTransform.right * m_pinata.PinataData.movementSpeed * horizontal * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AimRotation()
        {
            m_pawn.PawnTransform.forward = Vector3.Lerp(
                m_pawn.PawnTransform.forward,
                AllignPawnToCameraForward(),
                m_pinata.PinataData.rotationSpeedLerp);
        }

        /// <summary>
        /// Alligne le Pawn sur le forward de la camera.
        /// </summary>
        public Tween TweenAllignPawnOnCameraForward()
        {
            return DOTween.To(
            () => Pawn.PawnTransform.forward,
            x => Pawn.PawnTransform.forward = x,
            AllignPawnToCameraForward(),
            Pinata.PinataData.aimTransitionTime
            ).SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// Return le V3 pour que le Pawn s'alligne sur le forward de la camera.
        /// </summary>
        Vector3 AllignPawnToCameraForward()
        {
            return new Vector3(
                cameraTransform.forward.x,
                Pawn.PawnTransform.forward.y,
                cameraTransform.forward.z);
        }
    }
}