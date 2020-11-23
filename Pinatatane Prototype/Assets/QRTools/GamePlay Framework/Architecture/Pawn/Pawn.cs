using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace GameplayFramework
{
    /// <summary>
    /// This class is used for controllable objects
    /// </summary>
    public class Pawn : MyMonoBehaviour, IPawn
    {
        [SerializeField] protected Transform m_PawnTransform;
        public Transform PawnTransform { get => m_PawnTransform; set => m_PawnTransform = value; }

        [SerializeField] Controller m_Controller;
        public Controller Controller { get => m_Controller; set => m_Controller = value; }

        public override void OnStart()
        {
            base.OnStart();
        }

        /// <summary>
        /// Callback when you register to a controller
        /// </summary>
        public virtual void OnRegisterOnController()
        {
            
        }

        /// <summary>
        /// Callback when the pawn unregister to a controller
        /// </summary>
        public virtual void OnUnregisterOnController()
        {

        }
        
        protected override void OnGameStart()
        {

        }

        protected override void OnGameEnd()
        {

        }
    }
}