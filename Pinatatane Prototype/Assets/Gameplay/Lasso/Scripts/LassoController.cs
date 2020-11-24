using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using GameplayFramework;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class LassoController : MyMonoBehaviour
    {
        [SerializeField] Lasso m_Lasso;
        [SerializeField] LassoStateMachine m_LassoStateMachine;
        [SerializeField] Transform m_StartPosition;
        [SerializeField] PinataController m_PinataController;

        /** Variable lasso avec maillon */
        [SerializeField] GameObject m_Maillon;

        public bool debugMode = true;

        public Lasso Lasso { get => m_Lasso; set => m_Lasso = value; }
        public Transform StartPosition { get => m_StartPosition; set => m_StartPosition = value; }
        public PinataController PinataController { get => m_PinataController; set => m_PinataController = value; }
        public GameObject Maillon { get => m_Maillon; }

        public bool isConstructed = false;

        public override void OnStart()
        {
            InputManager.Instance.grabButton.onUp.AddListener(delegate { isConstructed = false; });

            m_LassoStateMachine.StartStateMachine(m_LassoStateMachine.currentState, this);
        }

        public override void OnUpdate()
        {
            m_LassoStateMachine.currentState.OnCurrent(this);
            m_LassoStateMachine.CheckCurrentState(this);
        }

        /// <summary>
        /// Retractation du grab.
        /// </summary>
        public void Retract()
        {
            if (debugMode) Debug.Log("<color=yellow>Lasso: </color> Retractation du lasso...");
            Lasso.LassoGraphics.Retract();
            Lasso.CurrenObjectGrabbed?.OnEndGrabbed();
            Lasso.CurrenObjectGrabbed = null;
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

            if(Lasso.CurrenObjectGrabbed != null)
            {
                Gizmos.DrawWireSphere(Lasso.CurrenObjectGrabbed.Transform.position, 1f);
                Gizmos.DrawLine(m_StartPosition.position, Lasso.CurrenObjectGrabbed.Transform.position);
            }
        }
    }
}

/*
 * NOTES:
 * C'EST LA STATE MACHINE QUI LANCE LE GRAB, DONC GERE LA LECTURE DES INPUTS
 */