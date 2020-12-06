using System.Collections;
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

        private Photon.Realtime.Player m_Player;

        public bool IsConnected { get => m_IsConnected; set => m_IsConnected = value; }
        public bool UseNetwork { get => m_UseNetwork; set => m_UseNetwork = value; }
        public bool DebugMessage { get => m_DebugMessage; set => m_DebugMessage = value; }
        public NetworkSettings NetworkSettings { get => m_NetworkSettings; set => m_NetworkSettings = value; }
        /// <summary>
        /// The Network Player define at the player instantiation.
        /// </summary>
        public Photon.Realtime.Player Player { get => m_Player; private set => m_Player = value; }

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                PhotonNetwork.Instantiate("Cible", new Vector3(5, 1, 5), Quaternion.identity);
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            IsConnected = true;

            if (DebugMessage) Debug.Log("<color=blue>Network: </color> Connected to Master.");
            m_Player = PhotonNetwork.LocalPlayer;
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            IsConnected = false;
        }
    }
}