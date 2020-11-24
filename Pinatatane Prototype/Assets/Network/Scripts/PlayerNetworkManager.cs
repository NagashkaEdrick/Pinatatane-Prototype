using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using Photon.Pun;

namespace GameplayFramework.Network
{
    public class PlayerNetworkManager : MonoBehaviourPunCallbacks
    {
        [SerializeField, BoxGroup("Network Infos")] string m_PlayerName = "DefaultName";

        [SerializeField, BoxGroup("Room Infos")] bool debugMessage = false;

        [SerializeField, BoxGroup("References")] Transform playerParent;

        public string PlayerName { get => m_PlayerName; set => m_PlayerName = value; }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            PhotonNetwork.LocalPlayer.NickName = m_PlayerName;
            if(debugMessage) Debug.Log(PhotonNetwork.LocalPlayer.NickName);

            AddPlayer();
        }

        public void AddPlayer()
        {
            GameObject newPlayer = PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
            newPlayer.name = "Player: " +  PhotonNetwork.LocalPlayer.NickName;
            newPlayer.transform.parent = playerParent;

            ConnectToCamera(newPlayer.transform);
        }

        void ConnectToCamera(Transform t)
        {
            CameraManager.Instance.SetCameraControllersTarget(t);
        }
    }
}