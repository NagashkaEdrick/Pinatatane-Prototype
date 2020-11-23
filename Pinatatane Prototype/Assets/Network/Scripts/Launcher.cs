using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;

using Sirenix.OdinInspector;

namespace GameplayFramework.Network
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField, BoxGroup("Lobby Infos")] bool debugMessage = false;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            if(debugMessage) Debug.Log("Connected to Master.");
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            if(debugMessage) Debug.Log("Join Lobby");
        }
    }
}