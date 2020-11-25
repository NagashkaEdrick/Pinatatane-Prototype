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

        [SerializeField] Transform m_CameraTransform;
        public Transform CameraTransform {
            get
            {
                if (m_CameraTransform == null)
                    m_CameraTransform = CameraManager.Instance.MainCamera.transform;
                return m_CameraTransform;
            }
            set => m_CameraTransform = value;
        }

        float 
            horizontal, 
            vertical, 
            moveAmount;

        /// <summary>
        /// Différent de IsControllable car ne bloque pas la lecture de la state machine.
        /// </summary>
        public bool IsBlocked { get; set; } = false;
        public bool BlockMovement { get; set; } = false;

        public override void OnStart()
        {
            base.OnStart();
            controllerStateMachine.StartStateMachine(controllerStateMachine.currentState ,this);
        }

        public override void Control(IPawn pawn)
        {
            controllerStateMachine.currentState.OnCurrent(this);
            //controllerStateMachine.CheckCurrentState(this);
        }

        /// <summary>
        /// Avancer tout droit.
        /// </summary>
        public void MoveForward(float speed)
        {
            horizontal = InputManager.Instance.moveX.JoystickValue;
            vertical = InputManager.Instance.moveY.JoystickValue;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            if (BlockMovement)
                return;

            Vector3 targetVelocity = m_pawn.PawnTransform.forward * speed * moveAmount * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Direction que le pawn doit prendre = Direction de la camera * axes des inputs;
        /// </summary>
        public void RotationBasedOnCameraOrientation(float speed)
        {
            Vector3 targetDir = CameraTransform.forward * vertical;
            targetDir += CameraTransform.right * horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = m_pawn.PawnTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(m_pawn.PawnTransform.rotation, tr, Time.deltaTime * moveAmount * speed);

            m_pawn.PawnTransform.rotation = targetRot;
        }
        
        /// <summary>
        /// Movement quand le joueur est en train de viser.
        /// </summary>
        public void AimMovement(float speed)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (BlockMovement)
                return;

            Vector3 targetVelocity = m_pawn.PawnTransform.forward * speed * vertical * Time.deltaTime;
            targetVelocity += m_pawn.PawnTransform.right * speed * horizontal * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Rotation quand le joueur est en train de viser
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
        public Tween TweenAllignPawnOnCameraForward(float duration)
        {
            return DOTween.To(
            () => Pawn.PawnTransform.forward,
            x => Pawn.PawnTransform.forward = x,
            AllignPawnToCameraForward(),
            duration
            ).SetEase(Ease.InOutSine);
        }

        /// <summary>
        /// Return le V3 pour que le Pawn s'alligne sur le forward de la camera.
        /// </summary>
        Vector3 AllignPawnToCameraForward()
        {
            return new Vector3(
                CameraTransform.forward.x,
                Pawn.PawnTransform.forward.y,
                CameraTransform.forward.z);
        }
    }
}