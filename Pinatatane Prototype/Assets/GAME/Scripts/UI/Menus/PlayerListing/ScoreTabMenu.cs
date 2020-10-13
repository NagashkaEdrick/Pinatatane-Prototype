using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinatatane
{
    public class ScoreTabMenu : UIMenu
    {
        public PhotonView photonView;
        public PlayerListingElement PlayerListingElementPrefab = default;
        [SerializeField] RectTransform container = default;

        public List<PlayerListingElement> playerListingElements = new List<PlayerListingElement>();

        public void AddPlayer(Player _player)
        {
            photonView.RPC("AddPlayerNetworking", RpcTarget.AllBuffered, _player);
        }

        [PunRPC]
        public void AddPlayerNetworking(Player _player)
        {
            GameObject go = PhotonNetwork.Instantiate("PlayerListingElementPrefab", transform.position, Quaternion.identity);
            go.transform.parent = container;
            PlayerListingElement newListingElement = go.GetComponent<PlayerListingElement>();
            playerListingElements.Add(newListingElement);
            newListingElement.Build(_player.NickName);
        }

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}