using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

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
            if (Input.GetKeyDown(KeyCode.A)) IsBlocked = !IsBlocked;

            controllerStateMachine.currentState.OnCurrent(this);
            controllerStateMachine.CheckCurrentState(this);
        }        
    }
}