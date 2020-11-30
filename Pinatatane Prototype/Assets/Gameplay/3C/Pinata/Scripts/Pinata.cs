using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using GameplayFramework;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class Pinata : Pawn, IBlockable, IReceiveDamages
    {        
        public PinataData PinataData = default;

        public LassoController LassoController;

        public Rigidbody Rigidbody;

        public PinataController PinataController;

        [SerializeField] PhotonView m_PhotonView;
        [SerializeField] float m_Health;

        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }
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

        public void ReceivedDamage(float damageTaken)
        {
            Debug.Log("Je reçois " + damageTaken + " damages");
            Health -= damageTaken;
        }
    }
}
