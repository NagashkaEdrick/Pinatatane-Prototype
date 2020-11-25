using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using GameplayFramework;

using Sirenix.OdinInspector;

namespace Pinatatane
{
    public class Pinata : Pawn
    {        
        public PinataData PinataData = default;

        public LassoController LassoController;

        [SerializeField] PhotonView m_PhotonView;

        [BoxGroup("A bouger ailleurs")]
        public Camera MainCamera;
        [BoxGroup("A bouger ailleurs")]
        public Image CrossHair;

        public PhotonView PhotonView { get => m_PhotonView; set => m_PhotonView = value; }

        public override void OnStart()
        {
            base.OnStart();

            Controller.RegisterPawn(this);
        }
    }
}
