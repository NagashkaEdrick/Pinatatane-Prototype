using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using Photon.Pun;
using Photon.Realtime;

namespace GameplayFramework.Network
{
    public class PlayerNetworkManager : MonoBehaviourPunCallbacks
    {
        public static PlayerNetworkManager Instance;

        [SerializeField, BoxGroup("Network Infos")] string m_PlayerName = "DefaultName";

        [SerializeField, BoxGroup("References")] Transform playerParent;

        public string PlayerName { get => m_PlayerName; set => m_PlayerName = value; }

        private void Awake()
        {
            Instance = this;
            m_PlayerName = "Player : " + Random.Range(0, 1000);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            PhotonNetwork.LocalPlayer.NickName = m_PlayerName;
            if(NetworkManager.Instance.DebugMessage) Debug.Log(PhotonNetwork.LocalPlayer.NickName);

            AddPlayer();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
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