﻿using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;

using TMPro;

namespace Pinatatane
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager Instance;

        public GameSettings gameSettings;
        public NetworkSettings networkSettings;

        [SerializeField] TextMeshProUGUI creationRoomText;

        public Camera offlineCamera;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            PhotonNetwork.GameVersion = GameSettings.Instance.GameVersion;
            PhotonNetwork.NickName = "Guest" + Random.Range(0, 999).ToString();
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to the server.");

            UIManager.Instance?.networkStatutElement.Online();

            if (!PhotonNetwork.InLobby)
                PhotonNetwork.JoinLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

            Debug.Log("Disconected to the server. Cause : " + cause);
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            Debug.Log("Created room succefully.");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);

            Debug.Log("Room creation failed : " + message);
        }

        public void CreateRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = networkSettings.playerCountMax;
            PhotonNetwork.JoinOrCreateRoom(creationRoomText.text, roomOptions, TypedLobby.Default);
        }

        public void CreateRoom(string _roomName)
        {
            if (!PhotonNetwork.IsConnected)
                return;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = networkSettings.playerCountMax;
            PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Room Joined.");

            offlineCamera.gameObject.SetActive(false);
            PlayerManager.Instance.CreatePlayer();
        }
    }
}