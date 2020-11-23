using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class LassoController : MyMonoBehaviour
    {
        [SerializeField] LassoData m_LassoData;
        [SerializeField] LassoStateMachine m_LassoStateMachine;
        [SerializeField] Transform m_StartPosition;
        [SerializeField] PinataController m_PinataController;
        [SerializeField, ReadOnly] IGrabbable m_CurrenObjectGrabbed;

        public LassoData LassoData { get => m_LassoData; set => m_LassoData = value; }
        public Transform StartPosition { get => m_StartPosition; set => m_StartPosition = value; }
        public PinataController PinataController { get => m_PinataController; set => m_PinataController = value; }
        public IGrabbable CurrenObjectGrabbed
        {
            get => m_CurrenObjectGrabbed;
            set
            {
                m_CurrenObjectGrabbed = value;
                if (m_CurrenObjectGrabbed != null)
                {
                    m_CurrenObjectGrabbed.OnStartGrabbed();
                }
            }
        }


        public override void OnStart()
        {
            InputManager.Instance.grabButton.onDown.AddListener(Grab);
            InputManager.Instance.grabButton.onUp.AddListener(Retract);

            m_LassoStateMachine.StartStateMachine(m_LassoStateMachine.currentState, this);
        }

        public override void OnUpdate()
        {
            m_LassoStateMachine.currentState.OnCurrent(this);
            m_LassoStateMachine.CheckCurrentState(this);
        }

        public void Grab()
        {

        }

        public void Retract()
        {
            m_CurrenObjectGrabbed = null;
        }

        protected override void OnGameEnd()
        {
        }

        protected override void OnGameStart()
        {
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if(m_CurrenObjectGrabbed != null)
            {
                Gizmos.DrawWireSphere(m_CurrenObjectGrabbed.Transform.position, 1f);
                Gizmos.DrawLine(m_StartPosition.position, m_CurrenObjectGrabbed.Transform.position);
            }
        }
    }
}