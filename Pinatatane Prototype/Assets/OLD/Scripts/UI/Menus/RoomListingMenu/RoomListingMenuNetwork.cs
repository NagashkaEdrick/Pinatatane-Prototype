using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace OldPinatatane
{
    public class RoomListingMenuNetwork : MonoBehaviourPunCallbacks
    {
        public RoomListingMenu menu;

        public RoomListingElement roomListingElement;

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("Room List Updated : " + roomList.Count + " rooms founded.");

            foreach(RoomInfo info in roomList)
            {
                RoomListingElement roomListing = Instantiate(roomListingElement, menu.roomListingContainer);
                if (roomListing != null)
                    roomListing.SetRoomInfo(info);
            }
        }
    }
}