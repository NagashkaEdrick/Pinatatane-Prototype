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
        [SerializeField, BoxGroup("Network Infos")] bool offlineMode;

        [SerializeField, BoxGroup("Lobby Infos")] bool debugMessage = false;
        [SerializeField, BoxGroup("Lobby Infos"), ReadOnly] bool isConnected = false;

        private void Start()
        {
            if (offlineMode)
                return;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            isConnected = true;

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

            isConnected = false;
        }
    }
}