using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class PlayerNetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] string m_PlayerName = "DefaultName";
        public string PlayerName { get => m_PlayerName; set => m_PlayerName = value; }
        [SerializeField, BoxGroup("Room Infos")] bool debugMessage = false;

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            PhotonNetwork.LocalPlayer.NickName = m_PlayerName;
            if(debugMessage) Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        }
    }
}