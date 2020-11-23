using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using Photon.Pun;
using Photon.Realtime;

namespace GameplayFramework.Network
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] NetworkSettings m_NetworkSettings = default;

        [SerializeField, BoxGroup("Room Infos"), ReadOnly] string currentRoomName = "Not in a room.";
        [SerializeField, BoxGroup("Room Infos")] bool debugMessage = false;

        [Button]
        public void CreateRoomTest()
        {
            CreateRoom("RoomTest");
        }

        public void CreateRoom(string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
            {
                Debug.Log("You are trying to create a room without name.");
                return;
            }

            RoomOptions newRoomOptions = new RoomOptions();
            newRoomOptions.MaxPlayers = m_NetworkSettings.MaxPlayerInRoom;

            PhotonNetwork.CreateRoom(roomName, newRoomOptions);
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
            if(debugMessage) Debug.Log("You join the room : " + PhotonNetwork.CurrentRoom.Name);
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