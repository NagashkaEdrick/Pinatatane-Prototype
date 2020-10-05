using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Pinatatane
{
    public class RoomListingElement : UIElement
    {
        [SerializeField] TextMeshProUGUI text;

        public void SetRoomInfo(RoomInfo roomInfo)
        {
            text.text = roomInfo.MaxPlayers + " , " + roomInfo.Name;
        }
    }
}