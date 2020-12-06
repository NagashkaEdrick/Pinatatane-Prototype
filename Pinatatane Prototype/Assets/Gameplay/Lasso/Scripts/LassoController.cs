using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;
using GameplayFramework.Network;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class LassoController : MyMonoBehaviour
    {
        [SerializeField] Lasso m_Lasso;
        [SerializeField] LassoStateMachine m_LassoStateMachine;
        [SerializeField] Transform m_StartPosition;
        [SerializeField] PinataController m_PinataController;

        public bool debugMode = true;

        public Lasso Lasso { get => m_Lasso; set => m_Lasso = value; }
        public Transform StartPosition { get => m_StartPosition; set => m_StartPosition = value; }
        public PinataController PinataController { get => m_PinataController; set => m_PinataController = value; }

        public bool isConstructed = false;
        public bool hasGrabbed = false;

        public override void OnStart()
        {
            InputManager.Instance.grabButton.onUp.AddListener(delegate { isConstructed = false; });

            m_LassoStateMachine.StartStateMachine(m_LassoStateMachine.currentState, this);
        }

        public override void OnUpdate()
        {
            if (NetworkManager.Instance.UseNetwork && PinataController.PhotonView.IsMine)
            {
                m_LassoStateMachine.currentState.OnCurrent(this);
                m_LassoStateMachine.CheckCurrentState(this);
            }
        }

        /// <summary>
        /// Appeller par l'object grabber -> Stop le grab.
        /// </summary>
        public void KillGrab()
        {
            Retract();
        }

        /// <summary>
        /// Retractation du grab.
        /// </summary>
        public void Retract()
        {
            if (debugMode) Debug.Log("<color=yellow>Lasso: </color> Retractation du lasso...");
            Lasso.LassoGraphics.Retract();
            Lasso.CurrenObjectGrabbed?.OnEndGrabbed();
            if (Lasso.CurrenObjectGrabbed != null) Lasso.CurrenObjectGrabbed.GrabbedBy = null;
            Lasso.CurrenObjectGrabbed = null;
            hasGrabbed = false;
        }

        protected override void OnGameEnd()
        {

        }

        protected override void OnGameStart()
        {

        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            if(Lasso.CurrenObjectGrabbed != null)
            {
                Gizmos.DrawWireSphere(Lasso.CurrenObjectGrabbed.Transform.position, 1f);
                Gizmos.DrawLine(m_StartPosition.position, Lasso.CurrenObjectGrabbed.Transform.position);
            }
        }
#endif
    }
}

/*
 * NOTES:
 * C'EST LA STATE MACHINE QUI LANCE LE GRAB, DONC GERE LA LECTURE DES INPUTS
 */