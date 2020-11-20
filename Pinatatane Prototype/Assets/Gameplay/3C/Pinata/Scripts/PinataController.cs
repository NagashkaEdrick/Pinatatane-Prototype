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
        public Pinata Pinata { get => m_pinata; set => m_pinata = value; }

        public StateMachinePinataController controllerStateMachine;

        public Transform cameraTransform = default;

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
    }
}