﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Sirenix.OdinInspector;

using GameplayFramework.Singletons;

namespace GameplayFramework.Network
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager Instance;

        [SerializeField, BoxGroup("Network Infos")] private bool m_UseNetwork;

        [SerializeField, BoxGroup("Network Infos"), ReadOnly] bool m_IsConnected = false;

        [SerializeField, BoxGroup("Debug Infos")] bool m_DebugMessage = false;

        [SerializeField, BoxGroup("Network Infos")] NetworkSettings m_NetworkSettings = default;


        public bool IsConnected { get => m_IsConnected; set => m_IsConnected = value; }
        public bool UseNetwork { get => m_UseNetwork; set => m_UseNetwork = value; }
        public bool DebugMessage { get => m_DebugMessage; set => m_DebugMessage = value; }
        public NetworkSettings NetworkSettings { get => m_NetworkSettings; set => m_NetworkSettings = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (!UseNetwork)
                return;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            IsConnected = true;

            if (DebugMessage) Debug.Log("<color=blue>Network: </color> Connected to Master.");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            IsConnected = false;
        }
    }
}