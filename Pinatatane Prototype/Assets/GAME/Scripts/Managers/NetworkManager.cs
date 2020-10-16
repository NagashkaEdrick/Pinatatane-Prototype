using System.Collections;
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
            InitServer();
        }

        #region Photon CallBack

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

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Room Joined.");

            offlineCamera?.gameObject.SetActive(false);
            UIManager.Instance?.FindMenu("RoomCreationMenu").Hide();
            PlayerManager.Instance.CreatePlayer();

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").AddPlayerNetworking(PhotonNetwork.PlayerList[i].UserId);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log(newPlayer.NickName + " est entré dans la room.");
            UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").AddPlayerNetworking(newPlayer.UserId);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            UIManager.Instance.FindMenu<ScoreTabMenu>("ScoreTabMenu").RemoveListingElement(otherPlayer.UserId);
        }

        #endregion

        public void InitServer()
        {
            PhotonNetwork.GameVersion = GameSettings.Instance.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void CreateRoom() => CreateRoom(creationRoomText.text);

        public void CreateRoom(string _roomName)
        {
            if (!PhotonNetwork.IsConnected)
                return;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = networkSettings.playerCountMax;
            roomOptions.PublishUserId = true;
            PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
        }
    }
}