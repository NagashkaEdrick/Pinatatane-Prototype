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

        private void Awake()
        {
            Instance = this;
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            if(NetworkManager.Instance.DebugMessage) Debug.Log("<color=blue>Network: </color> Join Lobby");
        }
    }
}