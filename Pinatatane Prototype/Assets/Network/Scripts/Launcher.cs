using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

using Sirenix.OdinInspector;
using Photon.Realtime;

namespace GameplayFramework.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public static Launcher Instance;

        [SerializeField, BoxGroup("Network Infos")] public bool offlineMode;

        [SerializeField, BoxGroup("Lobby Infos")] bool debugMessage = false;
        [SerializeField, BoxGroup("Lobby Infos"), ReadOnly] bool isConnected = false;

        public bool IsConnected { get => isConnected; set => isConnected = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (offlineMode)
                return;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            IsConnected = true;
            InputManager.Instance.useNetworkCommands = true;

            if(debugMessage) Debug.Log("<color=blue>Network: </color> Connected to Master.");
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            if(debugMessage) Debug.Log("<color=blue>Network: </color> Join Lobby");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            IsConnected = false;
        }
    }
}