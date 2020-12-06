using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using Photon.Pun;
using Photon.Realtime;
using System;

namespace GameplayFramework.Network
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField, BoxGroup("Network Infos")] bool createRoomAutomaticaly;

        [SerializeField, BoxGroup("Room Infos"), ReadOnly] string currentRoomName = "Not in a room.";

        private void Start()
        {
            if (createRoomAutomaticaly)
                StartCoroutine(CreateRoolAutomaticaly());
        }

        IEnumerator CreateRoolAutomaticaly()
        {
            yield return new WaitWhile(() => NetworkManager.Instance.IsConnected == true);
            Invoke("CreateRoomTest", 2f);
            yield break;
        }

        [Button]
        public void CreateRoomTest()
        {
            if (!NetworkManager.Instance.IsConnected)
                Debug.LogError("Impossible de créer la room car le NetworkManager n'est pas connecté.");

            CreateRoom("RoomTest");
        }

        public void CreateRoom(string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
            {
                Debug.Log("<color=blue>Network: </color> You are trying to create a room without name.");
                return;
            }

            if (PhotonNetwork.InRoom)
                return;

            RoomOptions newRoomOptions = new RoomOptions();
            newRoomOptions.MaxPlayers = NetworkManager.Instance.NetworkSettings.MaxPlayerInRoom;

            PhotonNetwork.JoinOrCreateRoom(roomName, newRoomOptions, TypedLobby.Default);
        }

        public void JoinRoom(RoomInfo roomInfo)
        {
            PhotonNetwork.JoinRoom(roomInfo.Name);
        }

        [Button]
        public void LeftRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnJoinedRoom()
        {
            if(NetworkManager.Instance.DebugMessage) Debug.Log("<color=blue>Network: </color> You join the room : " + PhotonNetwork.CurrentRoom.Name);
            currentRoomName = PhotonNetwork.CurrentRoom.Name;
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {

        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            currentRoomName = "Not in a room.";
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
        }
    }
}