using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace OldPinatatane
{
    public class RoomListingElement : UIElement
    {
        [SerializeField] TextMeshProUGUI text = default;
        [SerializeField] Button buttonJoin = default;
        public RoomInfo roomInfo;

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            this.roomInfo = roomInfo;
            text.text = roomInfo.MaxPlayers + " , " + roomInfo.Name;
            buttonJoin.onClick.AddListener(OnJoin);
        }

        void OnJoin()
        {
            NetworkManager.Instance.CreateRoom(roomInfo.Name);
        }
    }
}