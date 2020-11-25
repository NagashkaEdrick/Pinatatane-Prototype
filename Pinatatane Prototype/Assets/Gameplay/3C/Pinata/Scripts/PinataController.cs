using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using DG.Tweening;

using GameplayFramework;
using GameplayFramework.Network;

using Photon.Pun;

namespace Pinatatane
{
    /// <summary>
    /// This class is used to control the pinata.
    /// </summary>
    public class PinataController : PlayerController
    {
        [SerializeField] Pinata m_pinata = default;
        public Pinata Pinata { get => m_pinata; set => m_pinata = value; }

        [SerializeField] PhotonView m_PhotonView;

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

        /// <summary>
        /// Différent de IsControllable car ne bloque pas la lecture de la state machine.
        /// </summary>
        public bool IsBlocked { get; set; } = false;
        public bool BlockMovement { get; set; } = false;
        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }

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
        public void MoveForward(float speed)
        {
            if (NetworkManager.Instance.UseNetwork && m_PhotonView.IsMine)
            {
                Horizontal = InputManager.Instance.moveX.JoystickValue;
                Vertical = InputManager.Instance.moveY.JoystickValue;
            }
            else
            {
                Horizontal = InputManager.Instance.moveX.JoystickValue;
                Vertical = InputManager.Instance.moveY.JoystickValue;
            }
            MoveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal) + Mathf.Abs(Vertical));

            if (BlockMovement)
                return;

            Vector3 targetVelocity = m_pawn.PawnTransform.forward * speed * MoveAmount * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Direction que le pawn doit prendre = Direction de la camera * axes des inputs;
        /// </summary>
        public void RotationBasedOnCameraOrientation(float speed)
        {
            if (NetworkManager.Instance.UseNetwork && !m_PhotonView.IsMine)
                return;

            Vector3 targetDir = CameraTransform.forward * Vertical;
            targetDir += CameraTransform.right * Horizontal;
            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = m_pawn.PawnTransform.forward;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRot = Quaternion.Slerp(m_pawn.PawnTransform.rotation, tr, Time.deltaTime * MoveAmount * speed);

            m_pawn.PawnTransform.rotation = targetRot;
        }
        
        /// <summary>
        /// Movement quand le joueur est en train de viser.
        /// </summary>
        public void AimMovement(float speed)
        {
            if (NetworkManager.Instance.UseNetwork && m_PhotonView.IsMine)
            {
                Horizontal = InputManager.Instance.moveX.JoystickValue;
                Vertical = InputManager.Instance.moveY.JoystickValue;
            }
            else
            {
                Horizontal = InputManager.Instance.moveX.JoystickValue;
                Vertical = InputManager.Instance.moveY.JoystickValue;
            }

            if (BlockMovement)
                return;

            Vector3 targetVelocity = m_pawn.PawnTransform.forward * speed * Vertical * Time.deltaTime;
            targetVelocity += m_pawn.PawnTransform.right * speed * Horizontal * Time.deltaTime;
            m_pawn.PawnTransform.position += targetVelocity;
        }

        /// <summary>
        /// Rotation quand le joueur est en train de viser
        /// </summary>
        public void AimRotation()
        {
            if (NetworkManager.Instance.UseNetwork && !m_PhotonView.IsMine)
                return;

            m_pawn.PawnTransform.forward = Vector3.Lerp(
                m_pawn.PawnTransform.forward,
                AllignPawnToCameraForward(),
                m_pinata.PinataData.rotationSpeedLerp);
        }

        /// <summary>
        /// Alligne le Pawn sur le forward de la camera.
        /// </summary>
        public void TweenAllignPawnOnCameraForward(float duration)
        {
            if (NetworkManager.Instance.UseNetwork && !m_PhotonView.IsMine)
                return;

            DOTween.To(
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