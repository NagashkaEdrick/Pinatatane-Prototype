using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using GameplayFramework;

using Sirenix.OdinInspector;
using GameplayFramework.Network;

namespace Pinatatane
{
    public class Pinata : Pawn, IBlockable, IReceiveDamages, IGrabbable
    {        
        public PinataData PinataData = default;

        public LassoController LassoController;

        public PinataController PinataController;

        [SerializeField] PhotonView m_PhotonView;
        [SerializeField] float m_Health;
        [SerializeField] Rigidbody m_Rigidbody;
        [SerializeField] NetworkSharedTransform m_NetworkSharedTransform;
        [SerializeField] NetworkSharedComponents m_NetworkSharedComponents;

        public PhotonView PhotonView { get => m_PhotonView;}
        public bool IsBlocked { get => PinataController.IsBlocked; set => PinataController.IsBlocked = value; }
        public float Velocity { get => Rigidbody.velocity.magnitude;}
        public float Health {
            get => m_Health;
            set {
                m_Health = value;
                if (m_Health <= 0f)
                {
                    OnDeath();
                }
            }
        }

        public NetworkSharedTransform NetworkSharedTransform { get => m_NetworkSharedTransform; }
        public NetworkSharedComponents NetworkSharedComponents { get => m_NetworkSharedComponents; }
        public Transform Transform { get => m_PawnTransform; }
        public Rigidbody Rigidbody { get => m_Rigidbody;}

        public void OnDeath()
        {
            Debug.Log("Je meurs");
        }        

        public void OnEnter()
        {
            Debug.Log("Enter Block");
        }

        public void OnExit()
        {
            Debug.Log("Exit Block");
        }

        public override void OnStart()
        {
            base.OnStart();

            Controller.RegisterPawn(this);

            m_Health = PinataData.startHealth;
        }

        public void OnStartGrabbed()
        {
            m_NetworkSharedTransform.OverrideNetworkSharedTransform(m_NetworkSharedTransform);
            m_NetworkSharedComponents.OnStartGrab();
        }

        public void OnEndGrabbed()
        {
            m_NetworkSharedTransform.OverrideNetworkSharedTransformToDefaultPlayer();
            m_NetworkSharedComponents.OnEndGrab();
        }

        public void OnCurrentGrabbed()
        {
            
        }

        public void ReceivedDamage(float damageTaken)
        {
            Debug.Log("Je reçois " + damageTaken + " damages");
            Health -= damageTaken;
        }
    }
}
